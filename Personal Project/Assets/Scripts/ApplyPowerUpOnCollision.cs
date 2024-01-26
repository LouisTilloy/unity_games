using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ApplyPowerUpOnCollision : MonoBehaviour
{
    public PowerupManager powerupManager;
    [HideInInspector] int powerupIndex;

    public void SetIndex(int index)
    {
        powerupIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            powerupManager.powerupLevels[powerupIndex] += 1;
            Destroy(gameObject);
        }
    }
    
}
