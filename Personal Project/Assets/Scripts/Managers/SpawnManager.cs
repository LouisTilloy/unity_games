using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Levels configuration
    [SerializeField] TextAsset levelsJsonFile;
    // Levels internal variables
    Levels levelsConfig;
    int currentLevel = 0;
    Queue<float> spawnTimes;
    Queue<string> spawnEnemies;
    
    // Rocks
    [SerializeField] float rockSpawnInitialDelay;
    [SerializeField] float rockSpawnRate;
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
        spawnTimes = new Queue<float>();
        spawnEnemies = new Queue<string>();
        levelsConfig = JsonReader.ReadLevelsJson(levelsJsonFile);
        ScaleSpawnPosWithScreen();
        EventsHandler.OnScreenResolutionChange += ScaleSpawnPosWithScreen;
        
        // InvokeRepeating("SpawnRandomRockAtRandomPos", rockSpawnInitialDelay, rockSpawnRate);
        // InvokeRepeating("SpawnRandomPowerupAtRandomPos", powerupSpawnInitialDelay, powerupSpawnRate);
    }

    void Update()
    {
        // Levels handling - (levels start at 1, not 0)
        int nextLevel = currentLevel + 1;
        // If we are within the next level time interval, start the next level.
        if (nextLevel - 1 < levelsConfig.levels.Count && levelsConfig.levels[nextLevel - 1].timeInterval[0] <= clockTime)
        {
            AddRocksToQueue(nextLevel);
            currentLevel = nextLevel;
        }
        
        // Spawn handling.
        if (spawnTimes.Count > 0 && spawnTimes.Peek() <= clockTime)
        {
            spawnTimes.Dequeue();
            SpawnRockAtRandomPos(spawnEnemies.Dequeue());
        }

        clockTime += Time.deltaTime;
    }

    void AddRocksToQueue(int level)
    {
        Level levelInfo = levelsConfig.levels[level - 1];
       
        // Create a list of increasing times
        List<float> times = new List<float>();
        for (int timeIndex = 0; timeIndex < levelInfo.enemies.Count; timeIndex++)
        {
            times.Add(Random.Range((float)levelInfo.timeInterval[0], (float)levelInfo.timeInterval[1]));
        }
        times.Sort();

        // Add rocks at the given times in order
        for (int index = 0; index < times.Count; index++)
        {
            spawnEnemies.Enqueue(levelInfo.enemies[index]);
            spawnTimes.Enqueue(times[index]);
        }
    }

    void ScaleSpawnPosWithScreen()
    {
        xSpawnPos = xSpawnPos1080p * SharedUtils.AspectRatioScalingFactor();
    }

    private GameObject RandomRock()
    {
        int randomIndex = Random.Range(0, rocks.Length);
        return rocks[randomIndex];
    }

    private float RockSpawnYPosition(GameObject rock)
    {
        float bounceStrength = rock.GetComponent<BounceOnGround>().bounceStrength;
        return bounceStrength - 4.0f;  // found empirically to correspond with the max height
    }

    void SpawnRockAtRandomPos(string rockName)
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;  // -1 or 1
        GameObject rockPrefab = rocks[SharedUtils.RockNameToPrefabIndex(rockName)];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, RockSpawnYPosition(rockPrefab), rockPrefab.transform.position.z);
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        rock.GetComponent<Rigidbody>().AddForce(initialUpBoost * Vector3.up, ForceMode.Impulse);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
        rock.GetComponent<BounceOnWall>().isScriptActive = false;
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
        int randomIndex = Random.Range(0, powerups.Length);
        GameObject powerupPrefab = powerups[randomIndex];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, powerupYSpawnPos, powerupPrefab.transform.position.z);
        GameObject powerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        powerup.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;

        ApplyPowerUpOnCollision powerupScript = powerup.GetComponent<ApplyPowerUpOnCollision>();
        powerupScript.powerupManager = powerupManager;
        powerupScript.SetIndex(randomIndex);

    }

    private void OnDestroy()
    {
        EventsHandler.OnScreenResolutionChange -= ScaleSpawnPosWithScreen;
    }
}
