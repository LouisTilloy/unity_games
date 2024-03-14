using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shieldChargedClip;
    [SerializeField] float shieldChargedVolume;
    [SerializeField] AudioClip shieldBrokenClip;
    [SerializeField] float shieldBrokenVolume;

    void Start()
    {
        EventsHandler.OnShieldCharged += PlayChargedClip;
        EventsHandler.OnShieldBroken += PlayBrokenClip;
    }

    void PlayChargedClip()
    {
        audioSource.clip = shieldChargedClip;
        audioSource.volume = shieldChargedVolume;
        audioSource.Play();
    }

    void PlayBrokenClip()
    {
        audioSource.clip = shieldBrokenClip;
        audioSource.volume = shieldBrokenVolume;
        audioSource.pitch = 0.75f;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        EventsHandler.OnShieldCharged -= PlayChargedClip;
        EventsHandler.OnShieldBroken -= PlayBrokenClip;
    }
}
