using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArmies : MonoBehaviour
{
    List<Vector3> friendlySpawnPositions =
    new() {
            new Vector3(-2f,1.5f,-6f),
            new Vector3(-2f,-1.5f,-6f),
            new Vector3(-4f,1.5f,-6f),
            new Vector3(-4f,-1.5f,-6f),
            new Vector3(-6f,1.5f,-6f),
            new Vector3(-6f,-1.5f,-6f)
    };
    List<Vector3> enemySpawnPositions =
    new() {
            new Vector3(2f,1.5f,-6f),
            new Vector3(2f,-1.5f,-6f),
            new Vector3(4f,1.5f,-6f),
            new Vector3(4f,-1.5f,-6f),
            new Vector3(6f,1.5f,-6f),
            new Vector3(6f,-1.5f,-6f)
    };
    [SerializeField] GameObject friendlySoldierPrefab;
    [SerializeField] GameObject enemySoldierPrefab;
    
    public void Spawn(int nFriendly, int nEnemies)
    {
        for (int i = 0; i < nFriendly; i++)
        {
            Instantiate(friendlySoldierPrefab, friendlySpawnPositions[i], friendlySoldierPrefab.transform.rotation, transform.parent);
        }
        for (int i = 0; i < nEnemies; i++)
        {
            Instantiate(enemySoldierPrefab, enemySpawnPositions[i], enemySoldierPrefab.transform.rotation, transform.parent);
        }
    }
}
