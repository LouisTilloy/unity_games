using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnockbackPowerup : MonoBehaviour
{
    // References
    public GameObject powerIndicator;
    readonly private string powerupTag = "Powerup Knockback";

    // Dynamic Internal State
    private PowerupHelper powerupHelper;

    // Static parameters
    readonly private float powerupTime = 15;
    private float knockbackStrength = 20;
    private float knockbackScaleRateWithMass = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        powerupHelper = new GameObject().AddComponent<PowerupHelper>();
        powerupHelper.powerIndicator = powerIndicator;
        powerupHelper.actor = gameObject;
    }

    void LateUpdate()
    {
        powerupHelper.UpdateIndicatorPosition(-0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        powerupHelper.HelperOnTriggerEnter(other, powerupTag, powerupTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && powerupHelper.powerupActive)
        {
            SharedUtils.KnockbackCollide(gameObject, collision.gameObject, knockbackStrength, knockbackScaleRateWithMass);
        }
    }

}
