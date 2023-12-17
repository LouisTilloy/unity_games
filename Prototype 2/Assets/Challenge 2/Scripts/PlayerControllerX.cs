using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float dogSpawnMinInterval = 0.75f;
    private float timeSinceLastDogSpawned = 2.0f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastDogSpawned > dogSpawnMinInterval)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timeSinceLastDogSpawned = 0.0f;
        }
        timeSinceLastDogSpawned += Time.deltaTime;
    }
}
