using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

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
        audioSource.Stop();  // Stopping here seems to help with the pause/unpause bug removing sound
        audioSource.Play();
    }

    void OnDestroy()
    {
        EventsHandler.OnPowerupGrab -= PlayPowerupGrabSound;
    }
}
