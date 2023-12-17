using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BossSmashAttackPowerup : MonoBehaviour
{
    // References
    private ParticleSystem smashParticles;
    [HideInInspector]
    public GameObject powerIndicator;

    // Dynamic Internal State
    private PowerupHelper powerupHelper;
    private bool isJumping = false;
    private bool scriptInitialized = false;
    private float nextTimePowerActivates;

    // Static parameters
    readonly private float powerupWarmupTime = 2.0f;
    readonly private float[] powerTimeIntervalMinMax = { 0.5f, 4.0f };
    readonly private string targetsTag = "Player";
    readonly private float powerupTime = 1000;
    readonly private float attackLoadTime = 0.25f;
    readonly private float maxYPos = 6.0f;
    readonly private float baseForceStrength = 30.0f;
    readonly private float smashScaleRateWithMass = 0.75f;   // Higher values increase force
    readonly private float forceDecreaseRate = 4.0f;

    // Start is called before the first frame update
    private void Start()
    {
        nextTimePowerActivates = Time.time + RandomTime(powerTimeIntervalMinMax[0] + powerupWarmupTime, powerTimeIntervalMinMax[1] + powerupWarmupTime);
        powerupHelper = new GameObject("BOSS POWER HELPER").AddComponent<PowerupHelper>();
        smashParticles = GameObject.Find("BossSmashParticles").GetComponent<ParticleSystem>();
        EventsHandler.bossInitialLanding += smashParticles.Play;
        EventsHandler.bossLanding += smashParticles.Play;
    }

    public IEnumerator Initialize(GameObject powerIndicator)
    {
        yield return null;
        powerupHelper.powerIndicator = powerIndicator;
        powerupHelper.actor = gameObject;
        powerupHelper.ResetCoroutine(powerupTime);
        scriptInitialized = true;
    }

    private float RandomTime(float timeMin, float timeMax) 
    { 
        return Random.Range(timeMin, timeMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupHelper.powerupActive && Time.time > nextTimePowerActivates && !isJumping && transform.position.y >= 0)
        {
            StartCoroutine(SmashAttack());
            nextTimePowerActivates = Time.time + RandomTime(powerTimeIntervalMinMax[0], powerTimeIntervalMinMax[1]);
        }
    }

    void LateUpdate()
    {
        if (scriptInitialized)
        {
            powerupHelper.UpdateIndicatorPosition(-0.5f);
        }
        smashParticles.transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
    }
        
    private IEnumerator SmashAttack()
    {
        isJumping = true;
        yield return StartCoroutine(SharedUtils.JumpAnimation(transform, attackLoadTime, maxYPos));
        isJumping = false;
        smashParticles.Play();
        SharedUtils.ApplyJumpForce(transform, targetsTag, new SmashAttakParams(baseForceStrength, forceDecreaseRate, smashScaleRateWithMass));
        EventsHandler.InvokeBossLanding();
    }

    private void OnDestroy()
    {
        EventsHandler.bossInitialLanding -= smashParticles.Play;
        EventsHandler.bossLanding -= smashParticles.Play;
    }

}
