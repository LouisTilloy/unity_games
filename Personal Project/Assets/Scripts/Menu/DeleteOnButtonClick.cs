using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeleteOnButtonClick : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] List<GameObject> gameObjectsToDelete;

    void Start()
    {
        button.onClick.AddListener(DestroyGameObjects);
    }

    void DestroyGameObjects()
    {
        foreach(GameObject gameObjectInstance in gameObjectsToDelete)
        {
            Destroy(gameObjectInstance);
        }
    }
}
