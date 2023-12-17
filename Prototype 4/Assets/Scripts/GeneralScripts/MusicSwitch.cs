using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource bossMusic;
    public AudioSource gameOverMusic;
    public AudioSource gameOverInstantSound;

    private float maxVolume = 0.35f;
    private float maxVolumeGameOver = 0.5f;
    private float exitTime = 1.0f;
    private float startTime = 1.0f;
    private float exitTimeGameOver = 0.0f;
    private float startTimeGameOver = 0.0f;

    private AudioSource currentMusicPlaying;
    private AudioSource otherMusic;
    private IEnumerator currentCoroutine; 

    private void Start()
    {
        currentMusicPlaying = backgroundMusic;
        currentMusicPlaying.volume = maxVolume;
        otherMusic = bossMusic;
    }

    public void transitionMusic()
    {
        // Start music transition, and potentially stop the previous one if it was not done yet
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = SharedUtils.MixAudio(currentMusicPlaying, otherMusic, exitTime, startTime, maxVolume);
        StartCoroutine(currentCoroutine);

        // Keep trace of what music is currently playing
        AudioSource tmp = currentMusicPlaying;
        currentMusicPlaying = otherMusic;
        otherMusic = tmp;
    }

    public void transitionGameOverMusic()
    {
        otherMusic = gameOverMusic;
        exitTime = Mathf.Max(0.01f, exitTimeGameOver);
        startTime = Mathf.Max(0.01f, startTimeGameOver);
        maxVolume = maxVolumeGameOver;

        gameOverInstantSound.Play();
        transitionMusic();
    }

}
