using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    public bool isShopActive;
    public List<int> shopLevels;

    [SerializeField] List<GameObject> powerupPrefabs;
    [SerializeField] List<Vector3> powerupSpawnPosition;
    [SerializeField] PowerupManager powerupManager;
    [SerializeField] float timeBeforeSpawn;

    bool isShopInitalized = false;
    
    List<GameObject> shopSpawnedPowerups;

    void Start()
    {
        shopSpawnedPowerups = new List<GameObject>();
    }

    void Update()
    {
        if (!isShopActive) { return; }

        if (isShopActive && !isShopInitalized)
        {
            OpenShop();
            
        } 

        foreach(GameObject powerup in shopSpawnedPowerups)
        {
            if (powerup == null)
            {
                CloseShop();
                return;
            }
        }
    }

    void SpawnPowerups()
    {
        for (int loopIndex = 0; loopIndex < powerupSpawnPosition.Count; loopIndex++)
        {
            int powerupIndex = loopIndex % 2;
            Vector3 position = powerupSpawnPosition[loopIndex];
            GameObject powerupPrefab = powerupPrefabs[powerupIndex];
            GameObject spawnedPowerup = Instantiate(powerupPrefab, position, powerupPrefab.transform.rotation);
            spawnedPowerup.GetComponent<MoveRight>().horizontalSpeed = 0;

            ApplyPowerUpOnCollision powerupScript = spawnedPowerup.GetComponent<ApplyPowerUpOnCollision>();
            powerupScript.powerupManager = powerupManager;
            // powerupScript.SetIndex(powerupIndex);

            shopSpawnedPowerups.Add(spawnedPowerup);
        }
    }

    IEnumerator DelayedOpenSteps(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnPowerups();
    }

    void OpenShop()
    {
        isShopInitalized = true;
        StartCoroutine(DelayedOpenSteps(timeBeforeSpawn));
    }

    void CloseShop()
    {
        isShopActive = false;
        isShopInitalized = false;

        foreach (GameObject powerup in shopSpawnedPowerups)
        {
            if (powerup != null)
            {
                Destroy(powerup);
            }
        }

        shopSpawnedPowerups.Clear();
    }
}
