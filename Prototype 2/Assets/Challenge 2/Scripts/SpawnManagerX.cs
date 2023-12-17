using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startDelay = 1.0f;
    private float[] waitTimeRange = { 1.0f, 5.0f }; // default 4.0f

    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
        StartCoroutine(SpawnMultipleRandomBallWithDelay(startDelay, waitTimeRange));
    }

    IEnumerator SpawnMultipleRandomBallWithDelay(float initialWaitTime, float[] waitTimeRange)
    {
        yield return new WaitForSeconds(initialWaitTime);
        while (true)
        {
           yield return StartCoroutine(SpawnRandomBallWithRandomDelay(waitTimeRange));
        }
    }

    // Spawn random ball at random x position at top of play area
    IEnumerator SpawnRandomBallWithRandomDelay (float[] waitTimeRange)
    {
        // Wait some amount of time before spawning ball\
        float waitTime = Random.Range(waitTimeRange[0], waitTimeRange[1]);
        yield return new WaitForSeconds(waitTime);

        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        int ballIndex = Random.Range(0, ballPrefabs.Length);
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }
}
