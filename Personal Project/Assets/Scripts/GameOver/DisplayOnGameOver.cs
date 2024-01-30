using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnGameOver : MonoBehaviour
{
    void Awake()
    {
        EventsHandler.LateOnGameOver += ActivateGameObject;
        gameObject.SetActive(false);
    }

    private void ActivateGameObject()
    {
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventsHandler.LateOnGameOver -= ActivateGameObject;
    }
}
