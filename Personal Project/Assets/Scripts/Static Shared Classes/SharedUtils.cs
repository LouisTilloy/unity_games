using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SharedUtils
{

    public static IEnumerator WaitThenPauseGameForSeconds(float waitTime, float pauseTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        PauseGame();
        yield return new WaitForSecondsRealtime(pauseTime);
        UnPauseGame();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }

    public static IEnumerator MixAudio(
        AudioSource startAudio,
        AudioSource targetAudio,
        float exitTime,
        float startTime,
        float maxVolume
    )
    {
        float volume = startAudio.volume;
        while (volume > 0)
        {
            startAudio.volume = volume;
            volume -= Time.unscaledDeltaTime / exitTime;
            yield return null;
        }
        startAudio.Stop();

        volume = 0;

        targetAudio.Play();
        while (volume < maxVolume)
        {
            targetAudio.volume = volume;
            volume += Time.unscaledDeltaTime / startTime;
            yield return null;
        }
        targetAudio.volume = maxVolume;
    }

}
