using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Rocks
    [SerializeField] float rockSpawnInitialDelay;
    [SerializeField] float rockSpawnRate;
    [SerializeField] GameObject[] rocks;
    [SerializeField] float xSpawnPos;
    [SerializeField] float initialUpBoost;

    // Powerups
    [SerializeField] float powerupSpawnInitialDelay;
    [SerializeField] float powerupSpawnRate;
    [SerializeField] GameObject[] powerups;
    [SerializeField] PowerupManager powerupManager;
    float powerupYSpawnPos = 7.5f;

    // Total number of available powerups throughout the game
    public int PowerupCounts()
    {
        return powerups.Length;
    }

    void Start()
    {
        InvokeRepeating("SpawnRandomRockAtRandomPos", rockSpawnInitialDelay, rockSpawnRate);
        InvokeRepeating("SpawnRandomPowerupAtRandomPos", powerupSpawnInitialDelay, powerupSpawnRate);
    }

    private GameObject RandomRock()
    {
        int randomIndex = Random.Range(0, rocks.Length);
        return rocks[randomIndex];
    }

    private float RockSpawnYPosition(GameObject rock)
    {
        float bounceStrength = rock.GetComponent<BounceOnGround>().bounceStrength;
        return bounceStrength - 4.0f;  // found empirically to correspond with the max height
    }

    void SpawnRandomRockAtRandomPos()
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;  // -1 or 1
        GameObject rockPrefab = RandomRock();
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, RockSpawnYPosition(rockPrefab), rockPrefab.transform.position.z);
        GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        rock.GetComponent<Rigidbody>().AddForce(initialUpBoost * Vector3.up, ForceMode.Impulse);
        rock.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;
        rock.GetComponent<BounceOnWall>().isScriptActive = false;
    }

    void SpawnRandomPowerupAtRandomPos()
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;  // -1 or 1
        int randomIndex = Random.Range(0, powerups.Length);
        GameObject powerupPrefab = powerups[randomIndex];
        Vector3 spawnPosition = new Vector3(xSpawnPos * randomDirection, powerupYSpawnPos, powerupPrefab.transform.position.z);
        GameObject powerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
        powerup.GetComponent<MoveRight>().horizontalSpeed *= -1 * randomDirection;

        ApplyPowerUpOnCollision powerupScript = powerup.GetComponent<ApplyPowerUpOnCollision>();
        powerupScript.powerupManager = powerupManager;
        powerupScript.SetIndex(randomIndex);

    }
}