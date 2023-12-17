using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SmashAttackPowerup : MonoBehaviour
{
    // References
    public ParticleSystem smashParticles;
    public TextMeshProUGUI helperText;
    public GameObject powerIndicator;
    readonly private string powerupTag = "Powerup Smash";

    // Dynamic Internal State
    private PowerupHelper powerupHelper;
    private bool isJumping = false;

    // Static parameters
    readonly private string targetsTag = "Enemy";
    readonly private float powerupTime = 10.0f;
    readonly private float attackLoadTime = 0.25f;
    readonly private float maxYPos = 6.0f;
    readonly private float baseForceStrength = 15.0f;
    readonly private float smashScaleRateWithMass = 0.75f;   // Higher values increase force
    readonly private float forceDecreaseRate = 2.0f;
    
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
        if (powerupHelper.powerupActive && Input.GetKeyDown("space") && !isJumping && transform.position.y >= 0)
        {
            StartCoroutine(SmashAttack());
        }
        MaybeDisplayHelperMessageBlinking();
    }

    void LateUpdate()
    {
        powerupHelper.UpdateIndicatorPosition(-0.5f);
        smashParticles.transform.position = transform.position + new Vector3(0.5f, -0.5f, 0);
    }

    private IEnumerator SmashAttack()
    {
        isJumping = true;
        yield return StartCoroutine(SharedUtils.JumpAnimation(transform, attackLoadTime, maxYPos));
        isJumping = false;
        smashParticles.Play();
        SharedUtils.ApplyJumpForce(transform, targetsTag, new SmashAttakParams(baseForceStrength, forceDecreaseRate, smashScaleRateWithMass));
        EventsHandler.InvokePlayerLanding();
    }

    private void OnTriggerEnter(Collider other)
    {
        powerupHelper.HelperOnTriggerEnter(other, powerupTag, powerupTime);
    }

    private void MaybeDisplayHelperMessageBlinking()
    {
        if (!powerupHelper.powerupActive)
        {
            helperText.enabled = false;
            return;
        }
        int miliSeconds = (int)(Time.time * 1000);
        helperText.enabled = miliSeconds % 2000 < 1500;
    }

}
