using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWin : MonoBehaviour
{
    private void Start()
    {
        EventsHandler.OnWin += SelfDestroy;
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventsHandler.OnWin -= SelfDestroy;
    }
}
