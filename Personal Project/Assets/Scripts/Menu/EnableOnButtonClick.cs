using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableOnButtonClick : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] List<GameObject> gameObjectsToActivate;
    void Start()
    {
        button.onClick.AddListener(ActivateGameObjects);
    }

    void ActivateGameObjects()
    {
        foreach (GameObject gameObjectInstance in gameObjectsToActivate)
        {
            gameObjectInstance.SetActive(true);
        }
    }
}
