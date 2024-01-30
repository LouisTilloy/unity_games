using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    float currentTime;
    IEnumerator scoreClockCoroutine;

    public float Score()
    {
        return Mathf.Round(currentTime);
    }

    void Start()
    {
        scoreClockCoroutine = ScoreClock();
        StartCoroutine(scoreClockCoroutine);
        EventsHandler.OnGameOver += StopScoreClock;
    }

    void StopScoreClock()
    {
        StopCoroutine(scoreClockCoroutine);
    }

    IEnumerator ScoreClock()
    {
        currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnGameOver -= StopScoreClock;
    }
}
