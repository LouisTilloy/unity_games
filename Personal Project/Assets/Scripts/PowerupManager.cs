using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;
    public int[] powerupLevels;

    void Start()
    {
        powerupLevels = new int[spawnManager.PowerupCounts()];
        powerupLevels[0] = 1;  // First powerup - number of projectiles - starts at level 1
    }
}
