using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPooling;
    public float speed;
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

        // Clean list of any deactivated projectile
        launchedProjectiles.RemoveAll(projectile => !projectile.activeInHierarchy);

        // Shoot projectile
        GameObject lastProjectile;
        if (Input.GetButtonDown("Fire") && launchedProjectiles.Count < MaxProjectileCount())
        {
            lastProjectile = objectPooling.GetPooledObject();
            lastProjectile.transform.position = transform.position;
            lastProjectile.transform.rotation = transform.rotation;
            lastProjectile.SetActive(true);
            launchedProjectiles.Add(lastProjectile);

            EventsHandler.InvokeOnProjectileShot();
        }
    }
}
