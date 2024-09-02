using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] AudioSource mainMusicSource;
    [SerializeField] AudioSource fireworksMusicSource;
    [SerializeField] AudioClip winSong;
    [SerializeField] AudioClip fireworksSfx;
    [SerializeField] GameObject fireworksGameobject;
    [SerializeField] GameObject winningScreen;

    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.OnWin += TriggerWin;
    }

    private void OnDestroy()
    {
        EventsHandler.OnWin -= TriggerWin;
    }

    void TriggerWin()
    {
        fireworksGameobject.SetActive(true);
        winningScreen.SetActive(true);

        mainMusicSource.Stop();
        fireworksMusicSource.Stop();

        mainMusicSource.clip = winSong;
        fireworksMusicSource.clip = fireworksSfx;

        mainMusicSource.Play();
        fireworksMusicSource.Play();
    }
}
