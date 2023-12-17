using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerup : MonoBehaviour
{
    // This script should be assigned to the heart powerup directly
    private ManageLivesAndGameOver livesManager;
 
    // Start is called before the first frame update
    void Start()
    {
        livesManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ManageLivesAndGameOver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && livesManager.lives < livesManager.maxLives)
        {
            livesManager.lives++;
            Destroy(gameObject);
        }
    }
}
