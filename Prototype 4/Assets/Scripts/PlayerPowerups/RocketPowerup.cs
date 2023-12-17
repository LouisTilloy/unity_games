using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketPowerup : MonoBehaviour
{
    // References
    public AudioSource rocketLaunchSound;
    public GameObject powerIndicator;
    public GameObject rocketPrefab;
    readonly private string powerupTag = "Powerup Rockets";

    // Dynamic Internal State
    private PowerupHelper powerupHelper;
    private float rocketSpawnTimer = 0.0f;

    // Static parameters
    readonly private string targetTag = "Enemy";
    readonly private float powerupTime = 5;
    readonly private float rocketYSpawnPos = 1.0f;
    readonly private float rocketSpawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        powerupHelper = new GameObject().AddComponent<PowerupHelper>();
        powerupHelper.powerIndicator = powerIndicator;
        powerupHelper.actor = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupHelper.powerupActive && transform.position.y >= 0)
        {
            RocketSpawnLoop();
        }
    }

    void LateUpdate()
    {
        powerupHelper.UpdateIndicatorPosition(-0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        powerupHelper.HelperOnTriggerEnter(other, powerupTag, powerupTime);
    }

    private void RocketSpawnLoop()
    {
        GameObject[] rockets;
        if (rocketSpawnTimer >= rocketSpawnRate)
        {
            rockets = SharedUtils.RocketsSpawn(gameObject, targetTag, rocketPrefab, rocketYSpawnPos);
            if (rockets.Length != 0)
            {
                rocketLaunchSound.Play();
            }
            rocketSpawnTimer = 0.0f;
        }
        rocketSpawnTimer += Time.deltaTime;
    }

}
