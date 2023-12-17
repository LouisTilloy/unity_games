using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ManageLivesAndGameOver : MonoBehaviour
{
    
    [HideInInspector, System.NonSerialized]
    public int maxLives = 3;

    public int lives;
    private float yLimit = -50.0f;
    public bool gameOver = false;

    private string[] namesToDestroy = { "SpawnManager", "HelperTextSmash", "HelperTextArrows", "New Game Object", "BOSS POWER HELPER" };
    private string[] tagsToDestroy = { "Enemy", "Powerup Knockback", "Powerup Rockets", "Powerup Smash" , "Powerup Indicator", "Powerup Heart" };

    public TextMeshProUGUI gameOverText;
    [SerializeField]
    private AudioSource loseLifeAudio;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        lives = maxLives;
        gameOverText.enabled = false;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Waiting for next frame so that the sound is played where the player appears after death
    private IEnumerator playLoseLifeSound()
    {
        yield return new WaitForNextFrameUnit();
        loseLifeAudio.Play();
    }

    void resetPlayer()
    {
        transform.position = Vector3.zero;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yLimit)
        {
            lives -= 1;
            resetPlayer();
            if (lives > 0)
            {
                StartCoroutine(playLoseLifeSound());
            }
        }

        if (lives == 0)
        {
            gameOver = true;

        }

        if (gameOver)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // Display Game-Over Text
        gameOverText.enabled = true;

        // Switch to Game-Over Music
        GameObject.Find("Main Camera").GetComponent<MusicSwitch>().transitionGameOverMusic();

        // Destroy all gameobjects specified by name
        for (int nameIndex = 0; nameIndex < namesToDestroy.Length; nameIndex++)
        {
            Destroy(GameObject.Find(namesToDestroy[nameIndex]));
        }

        // Destroy all gameobjects specified by tag
        for (int tagIndex = 0; tagIndex < tagsToDestroy.Length; tagIndex++)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagsToDestroy[tagIndex]);
            for (int gameObjectIndex = 0; gameObjectIndex < gameObjects.Length; gameObjectIndex++)
            {
                Destroy(gameObjects[gameObjectIndex]);
            }
        }

        // Destroy player
        Destroy(gameObject);
    }
}
