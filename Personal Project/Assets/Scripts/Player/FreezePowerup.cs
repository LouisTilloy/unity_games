using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerup : MonoBehaviour
{
    PowerupManager powerupManager;
    float freezeActivatedTimer = 0;
    float timeToActivateScripts = 1.0f;

    void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
        foreach (GameObject projectile in ObjectPooling.SharedInstance.projectiles)
        {
            FreezeMovement freezeMovementScript = projectile.GetComponent<FreezeMovement>();
            freezeMovementScript.enabled = false;
        }
    }

    void Update()
    {
        // Only updating deactivated scripts, leaving some time to do so
        if (freezeActivatedTimer < timeToActivateScripts && powerupManager.powerupLevels[1] >= 1)
        {
            foreach (GameObject projectile in ObjectPooling.SharedInstance.projectiles)
            {
                if (!projectile.activeInHierarchy)
                {
                    FreezeMovement freezeMovementScript = projectile.GetComponent<FreezeMovement>();
                    freezeMovementScript.enabled = true;
                }
            }
            freezeActivatedTimer += Time.deltaTime;
        }
    }
}
