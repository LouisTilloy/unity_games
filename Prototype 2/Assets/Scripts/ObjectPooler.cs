using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<List<GameObject>> pooledObjects;
    public List<GameObject> objectsToPool;
    public List<int> amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // One list for each different prefab
        pooledObjects = new List<List<GameObject>>();
        for (int prefabIndex = 0; prefabIndex < amountToPool.Count; prefabIndex++)
        {
            pooledObjects.Add(new List<GameObject>());
            // Loop through list of pooled objects,deactivating them and adding them to the list 
            for (int i = 0; i < amountToPool[prefabIndex]; i++)
            {
                GameObject obj = (GameObject)Instantiate(objectsToPool[prefabIndex]);
                obj.SetActive(false);
                pooledObjects[prefabIndex].Add(obj);
                obj.transform.SetParent(this.transform); // set as children of Spawn Manager
            }
        }
    }

    public GameObject GetPooledObject(int prefabIndex)
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects[prefabIndex].Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[prefabIndex][i].activeInHierarchy)
            {
                if (prefabIndex == 1)
                {
                    pooledObjects[prefabIndex][i].GetComponent<IncreaseSize>().Initialize();
                }
                return pooledObjects[prefabIndex][i];
            }
        }
        // otherwise, return null   
        return null;
    }

}
