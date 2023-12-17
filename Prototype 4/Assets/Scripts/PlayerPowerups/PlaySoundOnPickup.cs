using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaySoundOnPickup : MonoBehaviour
{
    [SerializeField]
    private AudioClip clip;
    private AudioSource audioSource;
    [SerializeField]
    private float volume = 0.35f;

    // The coroutine being attached to the game object, it will be deleted if the object is deleted.
    // This ensures that in case the player walks on the power-up without actually activating it
    // (e.g. +1 life when full life), then the pickup sound is not played.
    private IEnumerator StopClipIfStillAlive()
    {
        yield return new WaitForNextFrameUnit();
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayClip();
            StartCoroutine(StopClipIfStillAlive());
        }
    }


    private void PlayClip()
    {
        // Create empty game object and make it play the sound
        GameObject emptyGameObject = new GameObject();
        emptyGameObject.transform.position = transform.position;
        audioSource = emptyGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.spatialBlend = 0;
        audioSource.Play();

        // Add a script to the game object to automatically destroy it once the sound is done playing
        audioSource.AddComponent<DestroyAfterMusicPlays>();
    }
}
