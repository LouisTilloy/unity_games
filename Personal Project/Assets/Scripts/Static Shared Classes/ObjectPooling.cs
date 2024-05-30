using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [HideInInspector] public List<GameObject> pooledObjects;
    [SerializeField] int nObjects = 10;

    void Awake()
    {
        pooledObjects = new List<GameObject>();
        for (int projectileIndex = 0; projectileIndex < nObjects; projectileIndex++)
        {
            GameObject projectile = Instantiate(objectPrefab);
            projectile.SetActive(false);
            projectile.transform.SetParent(gameObject.transform, false);
            pooledObjects.Add(projectile);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < nObjects; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

}
