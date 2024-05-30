using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public enum ObjectToSpawn
{
    Rock,
    Powerup
}


public class SpawnManager : MonoBehaviour
{
    // Levels configuration
    [SerializeField] InstanceJsonReader jsonReader;
    [SerializeField] float timeBeforeLevelTransition;

    // Levels internal variables
    List<Level> levelConfigs;
    int currentLevel = 1;
    int currentLevelChunk = 0;  // level chunks start at 1, so this is firstLevelChunk - 1
    Queue<float> rocksSpawnTimes;
    Queue<string> rocksToSpawn;
    Queue<float> powerupsSpawnTimes;
    Queue<string> powerupsToSpawn;

    // General
    List<GameObject> allSpawnedObjects;
    List<string> tagsOfInterest;
    bool levelLoading = false;

    // Rocks
    public List<ObjectPooling> rocksPooling;
    [SerializeField] float xBaseSpawnPos1080p;
    float xSpawnPos;
    [SerializeField] List<float> initialUpBoosts;

    // Powerups
    [SerializeField] float powerupSpawnInitialDelay;
    [SerializeField] float powerupSpawnRate;
    [SerializeField] GameObject[] powerups;
    [SerializeField] PowerupManager powerupManager;
    [SerializeField] float powerupYSpawnPos;

    // Internal clock
    float clockTime = 0;

    // Total number of available powerups throughout the game
    public int PowerupCounts()
    {
        return powerups.Length;
    }

    void Start()
    {
        allSpawnedObjects = new List<GameObject>();
        rocksSpawnTimes = new Queue<float>();
        rocksToSpawn = new Queue<string>();
        powerupsSpawnTimes = new Queue<float>();
        powerupsToSpawn = new Queue<string>();

        tagsOfInterest = SharedUtils.AllRockTags();
        tagsOfInterest.Add("Powerup");

        levelConfigs = jsonReader.ReadAllLevels();
        ScaleSpawnPosWithScreen();
        EventsHandler.OnScreenResolutionChange += ScaleSpawnPosWithScreen;

        EventsHandler.InvokeOnLevelTransition(currentLevel);
    }

    void Update()
    {
        // If levels are not loaded yet, wait until they are
        if (levelConfigs == null) { return; }

        // Stop update loop until next level is loaded
        if (levelLoading) { return; }

        // Levels handling - (levels start at 1, not 0)
        // - If we are within the next chunk time interval, start the next chunk of the level.
        if (NextLevelChunkIsDue())
        {
            int nextLevelChunk = currentLevelChunk + 1;
            AddToQueue(ObjectToSpawn.Rock, currentLevel, nextLevelChunk);
            AddToQueue(ObjectToSpawn.Powerup, currentLevel, nextLevelChunk);
           
            currentLevelChunk = nextLevelChunk;
        }
        
        // Rock spawn handling.
        if (rocksSpawnTimes.Count > 0 && rocksSpawnTimes.Peek() <= clockTime)
        {
            rocksSpawnTimes.Dequeue();
            GameObject rock = SpawnRockAtRandomPos(rocksToSpawn.Dequeue());
            allSpawnedObjects.Add(rock);
        }

        // Powerup spawn handling.
        if (powerupsSpawnTimes.Count > 0 && powerupsSpawnTimes.Peek() <= clockTime)
        {
            powerupsSpawnTimes.Dequeue();
            GameObject powerup = SpawnPowerupAtRandomPos(powerupsToSpawn.Dequeue());
            allSpawnedObjects.Add(powerup);
        }


        // If no more enemies and powerups are present switch to the next level.
        if (LastChunkOfLevelOver() && AllSpawnedObjectsDestroyed())
        {
            StartCoroutine(LoadNextLevelAfterTime(timeBeforeLevelTransition));
        }

        clockTime += Time.deltaTime;
    }

    bool AllSpawnedObjectsDestroyed()
    {
        foreach (string tag in tagsOfInterest)
        {
            if (GameObject.FindWithTag(tag) != null)
            {
                return false;
            }
        }
        return true;
        
    }

    bool LastChunkOfLevelOver()
    {
        return currentLevelChunk == levelConfigs[currentLevel - 1].levelsChunks.Count 
            && rocksToSpawn.Count == 0 
            && powerupsToSpawn.Count == 0;
    }

    bool NextLevelChunkIsDue()
    {
        int nextLevelChunk = currentLevelChunk + 1;
        return nextLevelChunk - 1 < levelConfigs[currentLevel - 1].levelsChunks.Count
               && levelConfigs[currentLevel - 1].levelsChunks[nextLevelChunk - 1].timeInterval[0] <= clockTime;
    }

    IEnumerator LoadNextLevelAfterTime(float time)
    {
        levelLoading = true;

        yield return new WaitForSeconds(time);
        
        allSpawnedObjects.Clear();
        clockTime = 0.0f;
        currentLevel++;
        currentLevelChunk = 0;
        levelLoading = false;

        EventsHandler.InvokeOnLevelTransition(currentLevel);
    }

    void AddToQueue(ObjectToSpawn objectToSpawn, int level, int levelChunk)
    {
        LevelChunk levelChunkInfo = levelConfigs[level - 1].levelsChunks[levelChunk - 1];
        
        // Select which queues to add to using what data
        List<string> levelChunkObjects;
        Queue<float> objectsSpawnTimes;
        Queue<string> objectsToSpawn;
        switch (objectToSpawn)
        {
            case ObjectToSpawn.Rock:
                levelChunkObjects = levelChunkInfo.enemies;
                objectsToSpawn = rocksToSpawn;
                objectsSpawnTimes = rocksSpawnTimes;
                break;
            case ObjectToSpawn.Powerup:
                levelChunkObjects = levelChunkInfo.powerups;
                objectsToSpawn = powerupsToSpawn;
                objectsSpawnTimes = powerupsSpawnTimes;
                break;
            default:
                throw new ArgumentException();
        }

        // Create a list of increasing times
        List<float> times = new List<float>();
        for (int timeIndex = 0; timeIndex < levelChunkObjects.Count; timeIndex++)
        {
            times.Add(UnityEngine.Random.Range((float)levelChunkInfo.timeInterval[0], (float)levelChunkInfo.timeInterval[1]));
        }
        times.Sort();

        // Add objects at the given times in order
        for (int index = 0; index < times.Count; index++)
        {
            objectsToSpawn.Enqueue(levelChunkObjects[index]);
            objectsSpawnTimes.Enqueue(times[index]);
        }
    }

    void ScaleSpawnPosWithScreen()
    {
        xSpawnPos = xBaseSpawnPos1080p * SharedUtils.AspectRatioScalingFactor();
    }

    private float RockSpawnYPosition(GameObject rock)
    {
        float bounceStrength = rock.GetComponent<BounceOnGround>().bounceStrength;
        return bounceStrength - 5.0f + rock.transform.localScale.x / 3;  // found empirically to correspond with the max height
    }

    GameObject SpawnRockAtRandomPos(string rockName)
    {
        // Choose if spawn from the right or from the left side.
        int randomDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;  // -1 or 1

        // Get a pooled rock corresponding to the name at a given positon.
        GameObject rock = rocksPooling[SharedUtils.RockNameToPrefabIndex(rockName)].GetPooledObject();
        Vector3 spawnPosition = new Vector3(
            (xSpawnPos + rock.transform.localScale.x / 3) * randomDirection, 
            RockSpawnYPosition(rock),
            rock.transform.position.z
        );
        rock.transform.position = spawnPosition;
        Rigidbody rockRigidbody = rock.GetComponent<Rigidbody>();
        rockRigidbody.velocity = Vector3.zero;
        rock.SetActive(true);

        // Setup rock components, including original force and velocity.
        float initialUpBoost = initialUpBoosts[SharedUtils.RockNameToPrefabIndex(rockName)];
        rockRigidbody.AddForce(initialUpBoost * Vector3.up, ForceMode.Impulse);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
        rock.GetComponent<BounceOnWall>().isScriptActive = false;
        
        // Return the instantiated rock.
        return rock;
    }

    GameObject SpawnPowerupAtRandomPos(string powerupName)
    {
        int randomDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;  // -1 or 1
        int powerupIndex = SharedUtils.PowerupNameToIndex(powerupName);
        GameObject powerupPrefab = powerups[powerupIndex];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, powerupYSpawnPos, powerupPrefab.transform.position.z);
        GameObject powerup = Instantiate(powerupPrefab, spawnPosition, powerupPrefab.transform.rotation);
        powerup.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;

        ApplyPowerUpOnCollision powerupScript = powerup.GetComponent<ApplyPowerUpOnCollision>();
        powerupScript.powerupManager = powerupManager;
        powerupScript.SetIndex(powerupIndex);

        return powerup;
    }

    private void OnDestroy()
    {
        EventsHandler.OnScreenResolutionChange -= ScaleSpawnPosWithScreen;
    }
}
