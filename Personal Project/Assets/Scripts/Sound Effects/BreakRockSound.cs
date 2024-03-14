using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRockSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    float[] timeIntervalStarts = { 0.333f, 2.666f, 4.166f};
    float[] timeIntervalEnds = { 1.5f, 3.333f, 5.666f};
    Coroutine coroutine;

    void Start()
    {
        EventsHandler.OnRockBroken += StartPlayCoroutine;
    }

    void StartPlayCoroutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(PlayRandomBreakSound());
    }

    IEnumerator PlayRandomBreakSound()
    {
        // Stop audio if there was any playing
        audioSource.Stop();

        // Play a random sound, each corresponding to an interval in the multi-sound source
        int randomIndex = Random.Range(0, timeIntervalStarts.Length);
        audioSource.time = timeIntervalStarts[randomIndex];
        audioSource.Play();

        // Stop at the end of the time interval
        float currentTime = audioSource.time;
        while (currentTime < timeIntervalEnds[randomIndex])
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
    }

    private void OnDestroy()
    {
        EventsHandler.OnRockBroken -= StartPlayCoroutine;
    }
}
