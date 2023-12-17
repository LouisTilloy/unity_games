using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // States
    public int maxLives = 3;
    public int currentLives = 3;
    public int score = 0;
    
    // Movement
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 10.0f;
    public float[] zRange = { -1.0f, 15.0f };
    private float projectilePosY = 1.0f;
    public float projectileOffsetX= 1.25f;

    // Power ups
    // Power up 0: multi cookies
    // Power up 1: piercing pizza
    public float powerUpTime = 5.0f;
    public bool[] powerUpStates = { false, false };
    public float powerUpPlayerScale = 1.5f;
    public float powerUpShootAngles = 20.0f;
    public float piercingDelay = 0.25f;
    public float piercingTimer;
    
    // Prefabs
    public GameObject regularProjectilePrefab;
    public GameObject piercingProjectilePrefab;
    public GameObject currentProjectilePrefab;
    private IEnumerator[] coroutines;

    // Start is called before the first frame update    
    void Start()
    {
        coroutines = new IEnumerator[powerUpStates.Length];
        for (int powerUpIndex = 0; powerUpIndex < powerUpStates.Length; powerUpIndex++)
        {
            coroutines[powerUpIndex] = ResetPlayerPowerUpAfterWait(powerUpIndex, powerUpTime);
        }

        piercingTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        KeepPlayerInBound();
        MovePlayer();

        // Piercing pizza
        if (powerUpStates[1])
        {
            currentProjectilePrefab = piercingProjectilePrefab;
        }
        else
        {
            currentProjectilePrefab = regularProjectilePrefab;
        }

        // If the piercing projectile power is on, there is a minimum delay between each shot
        if ((!powerUpStates[1] || piercingTimer >= piercingDelay))
        {
            shootProjectiles();
        }

        piercingTimer += Time.deltaTime;

    }

    public void looseOneLife()
    {
        // Lives cannot got below 0
        if (IsDead())
        {
            return;
        }
        
        // Lose 1 life and display the new number of lives
        currentLives -= 1;
    }

    public string playerLivesText()
    {
        string logMessage = "LIVES: ";
        for (int index = 0; index < maxLives; index++)
        {
            if (index < currentLives) {
                logMessage += "[o]";
            }
            else
            {
                logMessage += "[  ]";
            }
        }
        return logMessage;
    }

    public string playerScoreText()
    {
        return "SCORE: " + score.ToString();
    }

    public string playerGameOverText()
    {
        return "GAME OVER!\nFINAL SCORE:\n" + score.ToString();
    }

    public bool IsDead()
    {
        return currentLives == 0;
    }

    public void IncreaseScoreByOne()
    {
        // Cannot increase score if dead
        if (IsDead())
        {
            return;
        }
        score += 1;
    }

    void shootProjectiles()
    {
        // Shoot if player hit the space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile(0, 0);
            // multi cookies
            if (powerUpStates[0])
            {
                ShootProjectile(powerUpShootAngles, projectileOffsetX);
                ShootProjectile(-powerUpShootAngles, -projectileOffsetX);
            }
            if (powerUpStates[1])
            {
                piercingTimer = 0.0f;
            }
        }
    }

    // Keep player inside the playable area
    void KeepPlayerInBound()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.z < zRange[0])
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange[0]);
        }
        if (transform.position.z > zRange[1])
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange[1]);
        }
    }

    // Move player best on controller input
    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }

    // Shoot projectile with given X offset and angle
    void ShootProjectile(float angleInDegrees, float offsetX)
    {
        Vector3 projectilePos = new Vector3(transform.position.x + offsetX, projectilePosY, transform.position.z);
        GameObject projectileGameObject = (GameObject)Instantiate(currentProjectilePrefab, projectilePos, currentProjectilePrefab.transform.rotation);
        projectileGameObject.transform.RotateAround(transform.position, Vector3.up, angleInDegrees);
    }

    public void ApplyTemporaryPowerUp(int powerUpIndex)
    {
        // Turn on power up flag
        powerUpStates[powerUpIndex] = true;
        
        // Make player bigger
        transform.localScale = new Vector3(powerUpPlayerScale, powerUpPlayerScale, powerUpPlayerScale);

        
        // If the player already has the same power up, reset the duration
        StopCoroutine(coroutines[powerUpIndex]);

        // Reset power-up after some time
        coroutines[powerUpIndex] = ResetPlayerPowerUpAfterWait(powerUpIndex, powerUpTime);
        StartCoroutine(coroutines[powerUpIndex]);
    }

    IEnumerator ResetPlayerPowerUpAfterWait(int powerUpIndex, float waitTime)
    {   
        // Wait some time before resetting the power-up
        yield return new WaitForSeconds(waitTime);

        // Remove power up flag for the given power up index
        powerUpStates[powerUpIndex] = false;
        
        // Reset scale if no power-up is on
        if (powerUpStates[0] == false && powerUpStates[1] == false)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        
    }

}
