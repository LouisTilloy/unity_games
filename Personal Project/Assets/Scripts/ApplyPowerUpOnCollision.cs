using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowerUpOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("Powerup activated!");
            Destroy(gameObject);
        }
    }
}
