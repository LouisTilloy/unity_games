using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private Vector3 startPos = new Vector3(30, 0, 0);
    private float startDelay = 2;
    private float regularSpawnInterval = 3;
    private float timeSinceLastSpawn = 0.0f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameStarted)
        {
            TimeUpdate();
            MaybeSpawnObstacle();
        }
    }

    private void TimeUpdate()
    {
        // Artificially make time go faster for spawning purposes when dashing.
        if (playerControllerScript.dashing)
        {
            timeSinceLastSpawn += Time.deltaTime * playerControllerScript.dashMultiplier;
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
    }

    private void MaybeSpawnObstacle()
    {
        if (!playerControllerScript.gameOver && timeSinceLastSpawn >= regularSpawnInterval && Time.realtimeSinceStartup >= startDelay)
        {
            SpawnRandomObstacle();
            timeSinceLastSpawn = 0.0f;
        }
    }

    void SpawnRandomObstacle()
    {
        GameObject obstaclePrefab = RandomObstacle();
        Instantiate(obstaclePrefab, startPos, obstaclePrefab.transform.rotation);
    }

    GameObject RandomObstacle()
    {
        return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    }
}
