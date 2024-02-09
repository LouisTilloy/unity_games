using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ApplyPowerUpOnCollision : MonoBehaviour
{
    public PowerupManager powerupManager;
    [SerializeField] int powerupIndex;
    bool powerupApplied = false;

    public void SetIndex(int index)
    {
        powerupIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!powerupApplied && other.CompareTag("Projectile"))
        {
            powerupManager.powerupLevels[powerupIndex] += powerupManager.IsLevelMax(powerupIndex) ? 0 : 1;
            powerupApplied = true;
            EventsHandler.InvokeOnPowerupPickup();
            Destroy(gameObject);
        }
    }
    
}
