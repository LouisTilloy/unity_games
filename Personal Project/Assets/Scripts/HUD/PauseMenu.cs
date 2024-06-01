using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    
    AudioSource[] allAudioSources;
    List<AudioSource> playingAudioSources;
    List<float> playingTimes;
    GameObject pauseToolTips;
    GameObject playingToolTips;
    GameObject grayOverlay;
    bool pausingAvailable = true;
    
    [Tooltip("Read-only pause status.")]
    public bool isGamePaused;

    void Start()
    {
        pauseToolTips = transform.GetChild(1).gameObject;
        playingToolTips = transform.GetChild(2).gameObject;
        grayOverlay = transform.GetChild(0).gameObject;
        
        allAudioSources = mainCamera.GetComponents<AudioSource>();

        EventsHandler.OnGameOver += DisablePausingCapability;
    }

    void DisablePausingCapability()
    {
        pausingAvailable = false;
    }

    void Update()
    {
        if (pausingAvailable && Input.GetKeyDown(KeyCode.P))
        {
            PauseToggle();
        }
    }

    void PauseToggle()
    {
        isGamePaused = !isGamePaused;
        
        if (isGamePaused)
        {
            Time.timeScale = 0.0f;
            pauseToolTips.SetActive(true);
            grayOverlay.SetActive(true);
            playingToolTips.SetActive(false);
            PausePlayingAudioSources();
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseToolTips.SetActive(false);
            grayOverlay.SetActive(false);
            playingToolTips.SetActive(true);
            ResumePausedAudioSources();
        }
    }

    void PausePlayingAudioSources()
    {
        playingAudioSources = new List<AudioSource>();
        playingTimes = new List<float>();
        foreach (AudioSource source in allAudioSources)
        {
            if (source.isPlaying)
            {
                playingAudioSources.Add(source);
                playingTimes.Add(source.time);
                source.Stop();
            }
        }
    }

    void ResumePausedAudioSources()
    {
        AudioSource source;
        for (int sourceIndex = 0; sourceIndex < playingAudioSources.Count; sourceIndex++)
        {
            source = playingAudioSources[sourceIndex];
            source.Stop();  // Stopping again here seems to help with the pause/unpause bug removing sound
            source.time = playingTimes[sourceIndex];  // This is only necessary as a workaround of a pause bug in WebGL
            source.Play();
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnGameOver -= DisablePausingCapability;
    }
}
