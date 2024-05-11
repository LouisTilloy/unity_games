using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class SharedUtils
{

    public static List<string> AllRockTags()
    {
        return new List<string> { "Rock_Light", "Rock_Medium", "Rock_Big", "Rock_Giant", "Rock_SuperGiant", "Rock_UltraGiant", "Rock_MegaGiant" };
    }


    public static IEnumerator WaitDisplayAndFade(
        GameObject textObject, TextMeshProUGUI textComponent, float waitTime, float displayTime, float fadeTime
    )
    {
        yield return new WaitForSeconds( waitTime );
        yield return DisplayAndFade(textObject, textComponent, displayTime, fadeTime );
    }

    public static IEnumerator DisplayAndFade(GameObject textObject, TextMeshProUGUI textComponent, float displayTime, float fadeTime)
    {
        textComponent.alpha = 1.0f;
        textObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        float timer = 0;
        while (timer < fadeTime)
        {
            textComponent.alpha = Mathf.Lerp(1.0f, 0.0f, timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        textObject.SetActive(false);
    }

    public static int RockNameToPrefabIndex(string rockName)
    {
        switch (rockName)
        {
            case "Rock_Light":
                return 0;
            case "Rock_Medium":
                return 1;
            case "Rock_Big":
                return 2;
            case "Rock_Giant":
                return 3;
            case "Rock_SuperGiant":
                return 4;
            case "Rock_UltraGiant":
                return 5;
            case "Rock_MegaGiant":
                return 6;
        }
        throw new ArgumentException(rockName);
    }

    public static int PowerupNameToIndex(string powerupName)
    {
        switch (powerupName)
        {
            case "Extra_Projectile":
                return 0;
            case "Freeze":
                return 1;
            case "Shield":
                return 2;
            case "Random":
                return UnityEngine.Random.Range(0, 3);
        }
        throw new ArgumentException();
    }

    public static string PowerupIndexToName(int powerupIndex)
    {
        switch (powerupIndex)
        {
            case 0:
                return "Extra_Projectile";
            case 1:
                return "Freeze";
            case 2:
                return "Shield";
        }
        throw new ArgumentException();
    }

    // Resolution of reference: 1920x1080
    public static float refAspectRatio = 1920.0f / 1080.0f;

    public static float AspectRatioScalingFactor()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        return currentAspectRatio / refAspectRatio;
    }

    // Get maximum value of a dictionnary with a default value if dictionnary is empty.
    public static int MaxDictDefault(Dictionary<int, int> dict, int defaultValue)
    {
        if (dict.Count == 0) 
        {
            return defaultValue; 
        }
        else 
        {
            return dict.Values.Max(); 
        }
    }

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
