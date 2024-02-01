using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] SpawnManager spawnManager;

    public int[] powerupLevelsLimit;
    public int[] powerupLevels;

    void Start()
    {
        powerupLevels = new int[spawnManager.PowerupCounts()];
    }

    public bool IsLevelMax(int powerupIndex)
    {
        return powerupLevelsLimit[powerupIndex] == powerupLevels[powerupIndex];
    }
}
