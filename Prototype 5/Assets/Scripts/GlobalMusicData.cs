using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusicData : MonoBehaviour
{
    static public float currentVolume;
    static public float currentTime;
    static public bool initialized = false;

    static public void SetVolumeAndTime(float volume, float time)
    {
        currentVolume = volume;
        currentTime = time;
        initialized = true;
    }
}
