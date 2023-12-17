using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRocketPowerup : MonoBehaviour
{
    // References
    public GameObject rocketPrefab;
    public AudioSource rocketSound;

    // Dynamic Internal State
    private float rocketSpawnTimer = 0;
    private bool scriptInitialized = false;
    private PowerupHelper powerupHelper;

    // Static parameters
    readonly private string targetTag = "Player";
    readonly private float powerupTime = 1000;
    readonly private float rocketYSpawnPos = 1.0f;
    readonly private float rocketSpawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        powerupHelper = new GameObject("BOSS POWER HELPER").AddComponent<PowerupHelper>();
    }

    public IEnumerator Initialize(GameObject powerIndicator)
    {
        yield return null;
        powerupHelper.powerIndicator = powerIndicator;
        powerupHelper.actor = gameObject;
        powerupHelper.ResetCoroutine(powerupTime);
        scriptInitialized = true;
    }
    void LateUpdate()
    {
        if (scriptInitialized)
        {
            powerupHelper.UpdateIndicatorPosition(-0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupHelper.powerupActive && transform.position.y >= 0 && rocketSpawnTimer >= rocketSpawnRate)
        {
            rocketSound.Play();
            SharedUtils.RocketsSpawn(gameObject, targetTag, rocketPrefab, rocketYSpawnPos);
            rocketSpawnTimer = 0.0f;
        }
        rocketSpawnTimer += Time.deltaTime;
    }

}
