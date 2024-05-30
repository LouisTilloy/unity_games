using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    
    AudioSource[] allAudioSources;
    List<AudioSource> playingAudioSources;
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
        if (pausingAvailable && Input.GetKeyDown(KeyCode.Escape))
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
        foreach (AudioSource source in allAudioSources)
        {
            if (source.isPlaying)
            {
                playingAudioSources.Add(source);
                source.Pause();
            }
        }
    }

    void ResumePausedAudioSources()
    {
        foreach (AudioSource source in playingAudioSources)
        {
            source.Play();
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnGameOver -= DisablePausingCapability;
    }
}
