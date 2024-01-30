using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerup : MonoBehaviour
{
    PowerupManager powerupManager;
    FreezeMovement freezeMovementScript;
    bool wasFreezeActivated = false;

    void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
        freezeMovementScript = GetComponent<PlayerController>().projectilePrefab.GetComponent<FreezeMovement>();
        freezeMovementScript.enabled = false;
    }

    void Update()
    {
        if (!wasFreezeActivated && powerupManager.powerupLevels[1] >= 1)
        {
            freezeMovementScript.enabled = true;
            wasFreezeActivated = true;
        }
    }
}
