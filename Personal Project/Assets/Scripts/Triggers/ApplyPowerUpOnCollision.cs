using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ApplyPowerUpOnCollision : MonoBehaviour
{
    public PowerupManager powerupManager;
    [SerializeField] int powerupIndex;

    public void SetIndex(int index)
    {
        powerupIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Projectile"))
        {
            powerupManager.powerupLevels[powerupIndex] += 1;
            Destroy(gameObject);
        }
    }
    
}
