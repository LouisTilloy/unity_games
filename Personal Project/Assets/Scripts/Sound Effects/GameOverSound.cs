using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource gameOverMenuMusic;
    [SerializeField] AudioSource gameOverJingle;
    [SerializeField] AudioSource gameOverSoundEffect;

    private float silenceBeforeJingle = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.OnGameOver += PlayGameOverSounds;
        EventsHandler.LateOnGameOver += gameOverMenuMusic.Play;
    }

    void PlayGameOverSounds()
    {
        gameMusic.Stop();
        StartCoroutine(PlayDeathEffects());
    }

    IEnumerator PlayDeathEffects()
    {
        gameOverSoundEffect.Play();
        yield return new WaitForSecondsRealtime(silenceBeforeJingle);
        gameOverJingle.Play();
    }

    void OnDestroy()
    {
        EventsHandler.OnGameOver -= PlayGameOverSounds;
        EventsHandler.LateOnGameOver -= gameOverMenuMusic.Play;
    }
}
