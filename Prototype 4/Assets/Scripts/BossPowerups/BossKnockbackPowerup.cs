using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossKnockbackPowerup : MonoBehaviour
{
    // Dynamic Internal State
    private bool scriptInitialized = false;
    private PowerupHelper powerupHelper;

    // Static parameters
    readonly private float powerupTime = 1000;
    private float knockbackStrength = 10;
    private float knockbackScaleRateWithMass = 0.75f;

    // Start is called before the first frame update
    private void Start()
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

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) && powerupHelper.powerupActive)
        {
            SharedUtils.KnockbackCollide(gameObject, collision.gameObject, knockbackStrength, knockbackScaleRateWithMass);
        }
    }

}
