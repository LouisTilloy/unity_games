using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupGrabSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> powerupGrabClips;

    void Start()
    {
        EventsHandler.OnPowerupGrab += PlayPowerupGrabSound;
    }

    void PlayPowerupGrabSound(int powerupIndex)
    {
        audioSource.clip = powerupGrabClips[powerupIndex];
        audioSource.Play();
    }

    void OnDestroy()
    {
        EventsHandler.OnPowerupGrab -= PlayPowerupGrabSound;
    }
}
