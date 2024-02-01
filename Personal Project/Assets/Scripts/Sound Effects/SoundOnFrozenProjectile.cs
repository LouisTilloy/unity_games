using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnFrozenProjectile : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;

    void Start()
    {
        EventsHandler.OnProjectileFreeze += soundSource.Play;
    }

    private void OnDestroy()
    {
        EventsHandler.OnProjectileFreeze -= soundSource.Play;
    }
}
