using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum ObjectToSpawn
{
    Rock,
    Powerup
}


public class SpawnManager : MonoBehaviour
{
    // Levels configuration
    [SerializeField] TextAsset levelsJsonFile;
    // Levels internal variables
    Levels levelsConfig;
    int currentLevelChunk = -1;
    Queue<float> rocksSpawnTimes;
    Queue<string> rocksToSpawn;
    Queue<float> powerupsSpawnTimes;
    Queue<string> powerupsToSpawn;

    // Rocks
    [SerializeField] GameObject[] rocks;
    [SerializeField] float xSpawnPos1080p;
    float xSpawnPos;
    [SerializeField] float initialUpBoost;

    // Powerups
    [SerializeField] float powerupSpawnInitialDelay;
    [SerializeField] float powerupSpawnRate;
    [SerializeField] GameObject[] powerups;
    [SerializeField] PowerupManager powerupManager;
    float powerupYSpawnPos = 7.5f;

    // Internal clock
    float clockTime = 0;

    // Total number of available powerups throughout the game
    public int PowerupCounts()
    {
        return powerups.Length;
    }

    void Start()
    {
        rocksSpawnTimes = new Queue<float>();
        rocksToSpawn = new Queue<string>();
        powerupsSpawnTimes = new Queue<float>();
        powerupsToSpawn = new Queue<string>();

        levelsConfig = JsonReader.ReadLevelsJson(levelsJsonFile);
        ScaleSpawnPosWithScreen();
        EventsHandler.OnScreenResolutionChange += ScaleSpawnPosWithScreen;
    }

    void Update()
    {
        // Levels handling - (levels start at 1, not 0)
        int nextLevelChunk = currentLevelChunk + 1;
        // If we are within the next level time interval, start the next level.
        if (nextLevelChunk < levelsConfig.levelsChunks.Count && levelsConfig.levelsChunks[nextLevelChunk].timeInterval[0] <= clockTime)
        {
            AddToQueue(ObjectToSpawn.Rock, nextLevelChunk);
            AddToQueue(ObjectToSpawn.Powerup, nextLevelChunk);
           
            currentLevelChunk = nextLevelChunk;
        }
        
        // Rock spawn handling.
        if (rocksSpawnTimes.Count > 0 && rocksSpawnTimes.Peek() <= clockTime)
        {
            rocksSpawnTimes.Dequeue();
            SpawnRockAtRandomPos(rocksToSpawn.Dequeue());
        }

        // Powerup spawn handling
        if (powerupsSpawnTimes.Count > 0 && powerupsSpawnTimes.Peek() <= clockTime)
        {
            powerupsSpawnTimes.Dequeue();
            SpawnPowerupAtRandomPos(powerupsToSpawn.Dequeue());
        }


        clockTime += Time.deltaTime;
    }


    void AddToQueue(ObjectToSpawn objectToSpawn, int levelChunk)
    {
        LevelChunk levelChunkInfo = levelsConfig.levelsChunks[levelChunk];
        
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
        xSpawnPos = xSpawnPos1080p * SharedUtils.AspectRatioScalingFactor();
    }

    private float RockSpawnYPosition(GameObject rock)
    {
        float bounceStrength = rock.GetComponent<BounceOnGround>().bounceStrength;
        return bounceStrength - 4.0f;  // found empirically to correspond with the max height
    }

    void SpawnRockAtRandomPos(string rockName)
    {
        int randomDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;  // -1 or 1
        GameObject rockPrefab = rocks[SharedUtils.RockNameToPrefabIndex(rockName)];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, RockSpawnYPosition(rockPrefab), rockPrefab.transform.position.z);
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        rock.GetComponent<Rigidbody>().AddForce(initialUpBoost * Vector3.up, ForceMode.Impulse);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
        rock.GetComponent<BounceOnWall>().isScriptActive = false;
    }

    void SpawnPowerupAtRandomPos(string powerupName)
    {
        int randomDirection = UnityEngine.Random.Range(0, 2) * 2 - 1;  // -1 or 1
        int powerupIndex = SharedUtils.PowerupNameToIndex(powerupName);
        GameObject powerupPrefab = powerups[powerupIndex];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, powerupYSpawnPos, powerupPrefab.transform.position.z);
        GameObject powerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        powerup.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;

        ApplyPowerUpOnCollision powerupScript = powerup.GetComponent<ApplyPowerUpOnCollision>();
        powerupScript.powerupManager = powerupManager;
        powerupScript.SetIndex(powerupIndex);

    }

    private void OnDestroy()
    {
        EventsHandler.OnScreenResolutionChange -= ScaleSpawnPosWithScreen;
    }
}
