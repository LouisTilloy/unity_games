using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    public bool isShopActive;
    public List<int> shopLevels;

    [SerializeField] float timeBeforeShopAutoCloses;
    [SerializeField] List<GameObject> powerupPrefabs;
    [SerializeField] List<Vector3> powerupSpawnPosition;
    [SerializeField] PowerupManager powerupManager;
    [SerializeField] PlayerController playerController;
    [SerializeField] float timeBeforeSpawn;

    bool isShopInitalized = false;
    float timeSinceShopOppened = 0.0f;
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

        // Close shop if one of the powerup was chosen
        foreach(GameObject powerup in shopSpawnedPowerups)
        {
            if (powerup == null)
            {
                CloseShop();
                return;
            }
        }

        // Otherwise close shop if a long time has passed to prevent soft lock
        if (timeSinceShopOppened > timeBeforeShopAutoCloses)
        {
            CloseShop();
        }

        timeSinceShopOppened += Time.deltaTime;
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
        playerController.DeleteAllActiveProjectiles();
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
