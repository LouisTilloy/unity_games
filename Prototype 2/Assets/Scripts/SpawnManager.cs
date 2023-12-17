using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Animals general spawn
    public GameObject[] animalPrefabs;
    public GameObject[] aggressiveAnimalPrefabs;
    private GameObject instantiatedAnimal;
    private float maxDifficultyTime = 90.0f; // default 60.0f

    // Vertical animals parameters
    public float spawnRangeX = 20.0f;
    private float spawnPosZ = 20.0f;
    private float verticalSpawnStartDelay = 2.0f;
    private float[] verticalSpawnIntervalRange = { 5.0f, 1.0f };  // Depends on difficulty, will go from [0] to [1] in maxDifficultyTime seconds

    // Horizontal animals parameters
    private float[] spawnRangeZ = { -1.0f, 15.0f };
    private float spawnPosX = 30.0f;
    private float horizontalSpawnStartDelay = 28.8f;
    private float[] horizontalSpawnIntervalRange = {5.0f, 1.0f};  // Depends on difficulty too

    // Extra animal spawns
    public int slowAnimalIndex = 2;
    private float[] slowProbabilityRange = { 0.0f, 0.30f };
    public float currentSlowProbability;
    private float slowSpawnInterval = 0.97f;

    // Power up
    public GameObject[] powerUpPrefabs;
    private float[] powerUpProbabilityRange = { 0.05f, 0.16f };
    private float powerUpSpawnInterval = 1.73f;

    // Storing Player GameObject to be used by spawned animals
    public GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRandomAnimalVertical", verticalSpawnStartDelay);
        InvokeRepeating("MaybeSpawnRandomSlowAnimalVertical", verticalSpawnStartDelay, slowSpawnInterval);
        InvokeRepeating("MaybeSpawnPowerUp", verticalSpawnStartDelay, powerUpSpawnInterval);
        Invoke("SpawnRandomAnimalHorizontal", horizontalSpawnStartDelay);
    }

    // Update is called once per frame
    void Update()
    {
        currentSlowProbability = Mathf.Lerp(slowProbabilityRange[0], slowProbabilityRange[1], Time.time / maxDifficultyTime);
    }

    GameObject InstantiateAndTransferPlayerReference(GameObject original, Vector3 position, Quaternion rotation)
    {
        instantiatedAnimal = Instantiate(original, position, rotation);
        // Give player reference to different components for lives and score management;
        DestroyOutOfBounds component1 = instantiatedAnimal.GetComponent<DestroyOutOfBounds>();
        if (component1 != null)
        {
            component1.player = player; 
        }
        DetectCollisions component2 = instantiatedAnimal.GetComponent<DetectCollisions>();
        if (component2 != null)
        {
            component2.player = player;
        }
        DetectFoodCollisions component3 = instantiatedAnimal.GetComponent<DetectFoodCollisions>();
        if (component3 != null)
        {
            component3.player = player;
        }
        return instantiatedAnimal;
    }

    // Spawn specific animal at a random position at the top of the screen, traveling vertically
    void SpawnSpecificAnimalVertical(int animalIndex)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        InstantiateAndTransferPlayerReference(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }

    // Spawn specific animal at a random position on either side of the screen, traveling horizontally
    void SpawnSpecificAnimalHorizontal(int animalIndex)
    {
        float spawnSide = Mathf.Sign(Random.Range(-1.0f, 1.0f));  // -1 or 1, 50% of the time. -1 = left, 1 = right
        Vector3 spawnPos = new Vector3(spawnSide * spawnPosX, 0, Random.Range(spawnRangeZ[0], spawnRangeZ[1]));
        instantiatedAnimal = InstantiateAndTransferPlayerReference(
            aggressiveAnimalPrefabs[animalIndex], 
            spawnPos, 
            aggressiveAnimalPrefabs[animalIndex].transform.rotation
        );
        instantiatedAnimal.transform.Rotate(Vector3.up, spawnSide * 90.0f);
    }

    // Spawn random animal at a random position (vertical)
    void SpawnRandomAnimalVertical()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        SpawnSpecificAnimalVertical(animalIndex);

        Invoke(
            "SpawnRandomAnimalVertical",
            Mathf.Lerp(verticalSpawnIntervalRange[0], verticalSpawnIntervalRange[1], Time.time / maxDifficultyTime)
        );
    }

    // Spawn random animal at a random position (horizontal)
    void SpawnRandomAnimalHorizontal()
    {
        int animalIndex = Random.Range(0, aggressiveAnimalPrefabs.Length);
        SpawnSpecificAnimalHorizontal(animalIndex);

        Invoke(
            "SpawnRandomAnimalHorizontal",
            Mathf.Lerp(horizontalSpawnIntervalRange[0], horizontalSpawnIntervalRange[1], Time.time / maxDifficultyTime)
        );
    }


    // Spawn a cow at random with a given probability
    void MaybeSpawnRandomSlowAnimalVertical()
    {
        float randomRoll = Random.Range(0.0f, 1.0f);
        if (randomRoll < currentSlowProbability)
        {
            SpawnSpecificAnimalVertical(slowAnimalIndex);
        }
    }

    // Spawn power-up with a given probability
    void MaybeSpawnPowerUp()
    {
        float randomRoll = Random.Range(0.0f, 1.0f);
        float powerUpProbability = Mathf.Lerp(powerUpProbabilityRange[0], powerUpProbabilityRange[1], Time.time / maxDifficultyTime);
        if (randomRoll < powerUpProbability)
        {
            int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpPrefab = powerUpPrefabs[powerUpIndex];
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), powerUpPrefab.transform.position.y, spawnPosZ);
            Instantiate(powerUpPrefab, spawnPos, powerUpPrefab.transform.rotation);
        }
    }
}
