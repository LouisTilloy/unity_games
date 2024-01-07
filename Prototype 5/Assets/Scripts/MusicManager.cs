using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private Slider volumeSlider;
    private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();
        backgroundMusic = GetComponent<AudioSource>();
        
        if (GlobalMusicData.initialized)
        {
            volumeSlider.value = GlobalMusicData.currentVolume;
            backgroundMusic.time = GlobalMusicData.currentTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMusic.volume = volumeSlider.value;
    }
}
