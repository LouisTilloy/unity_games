using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<float> pitchRandomRange;
    void Start()
    {
        EventsHandler.OnProjectileShot += playWithRandomPitch;
    }

    void playWithRandomPitch()
    {
        audioSource.pitch = Random.Range(pitchRandomRange[0], pitchRandomRange[1]);
        audioSource.Stop();  // Seems to help with a bug with the pause menu and the audioSource not starting from t=0
        audioSource.Play();
    }

    private void OnDestroy()
    {
        EventsHandler.OnProjectileShot -= playWithRandomPitch;
    }
}
