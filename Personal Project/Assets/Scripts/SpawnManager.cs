using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Rocks
    [SerializeField]
    private GameObject[] rocks;
    [SerializeField]
    private float xSpawnPos;
    [SerializeField]
    private float initialUpBoost;
    
    // Powerups
    [SerializeField]
    private GameObject[] powerups;
    private float powerupYSpawnPos = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomRockAtRandomPos", 1.0f, 5.0f);
        InvokeRepeating("SpawnRandomPowerupAtRandomPos", 5.5f, 12.78412f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject RandomRock()
    {
        int randomIndex = Random.Range(0, rocks.Length);
        return rocks[randomIndex];
    }

    private GameObject RandomPowerup()
    {
        int randomIndex = Random.Range(0, powerups.Length);
        return powerups[randomIndex];
    }

    private float RockSpawnYPosition(GameObject rock)
    {
        float bounceStrength = rock.GetComponent<BounceOnGround>().bounceStrength;
        return bounceStrength - 4.0f;  // found empirically to correspond with the max height
    }


    void SpawnRandomRockAtRandomPos()
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;  // -1 or 1
        GameObject rockPrefab = RandomRock();
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, RockSpawnYPosition(rockPrefab), rockPrefab.transform.position.z);
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        rock.GetComponent<Rigidbody>().AddForce(initialUpBoost * Vector3.up, ForceMode.Impulse);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
        rock.GetComponent<BounceOnWall>().isScriptActive = false;
    }

    void SpawnRandomPowerupAtRandomPos()
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;  // -1 or 1
        GameObject powerupPrefab = RandomPowerup();
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, powerupYSpawnPos, powerupPrefab.transform.position.z);
        GameObject rock = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
    }
}
