using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameOver : MonoBehaviour
{
    private void Start()
    {
        EventsHandler.LateOnGameOver += SelfDestroy;
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventsHandler.LateOnGameOver -= SelfDestroy;
    }
}
