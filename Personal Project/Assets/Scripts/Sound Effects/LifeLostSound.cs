using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeLostSound : MonoBehaviour
{
    [SerializeField] AudioSource lifeLostSound;
    void Start()
    {
        EventsHandler.OnLifeLost += lifeLostSound.Play;
    }

    void OnDestroy()
    {
        EventsHandler.OnLifeLost -= lifeLostSound.Play;
    }
}
