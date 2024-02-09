using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;

    [SerializeField] GameObject projectilePrefab;
    public List<GameObject> projectiles;
    int nProjectiles = 10;

    void Awake()
    {
        SharedInstance = this;

        projectiles = new List<GameObject>();
        for (int projectileIndex = 0; projectileIndex < nProjectiles; projectileIndex++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectile.transform.SetParent(gameObject.transform, false);
            projectiles.Add(projectile);
        }
    }

    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < nProjectiles; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return projectiles[i];
            }
        }
        return null;
    }

}
