using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ApplyPowerUpOnCollision : MonoBehaviour
{
    public PowerupManager powerupManager;
    [SerializeField] int powerupIndex;
    bool powerupApplied = false;
    Vector3 position;

    public void SetIndex(int index)
    {
        powerupIndex = index;
    }

    private void Update()
    {
        position = transform.position;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!powerupApplied && other.CompareTag("Projectile"))
        {
            powerupManager.powerupLevels[powerupIndex] += powerupManager.IsLevelMax(powerupIndex) ? 0 : 1;
            powerupApplied = true;
            // Using transform.position instead of position here does not yield the correct position somehow,
            // maybe some edge case with when OnTriggerEnter is launched (the Physics tics)
            EventsHandler.InvokeOnPowerupGrab(powerupIndex, position);
            Destroy(gameObject);
        }
    }
    
}
