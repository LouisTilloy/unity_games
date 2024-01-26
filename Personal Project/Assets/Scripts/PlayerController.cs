using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [SerializeField] GameObject projectilePrefab;
    List<GameObject> launchedProjectiles;
    PowerupManager powerupManager;

    private void Start()
    {
        launchedProjectiles = new List<GameObject>();
        powerupManager = GetComponent<PowerupManager>();
    }

    int MaxProjectileCount()
    {
        return powerupManager.powerupLevels[0] + 1;
    }

    void Update()
    {
        // Left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        // Clean list of any null projectile
        launchedProjectiles.RemoveAll(prefab => prefab == null);

        // Shoot projectile
        if (Input.GetButtonDown("Fire") && launchedProjectiles.Count < MaxProjectileCount())
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, projectilePrefab.transform.position.y, transform.position.z);
            launchedProjectiles.Add(Instantiate(projectilePrefab, spawnPosition, transform.rotation));
        }
    }
}
