using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] int scoreLight;
    [SerializeField] int scoreMedium;
    [SerializeField] int scoreBig;
    [SerializeField] int scoreGiant;

    public float Score()
    {
        return score;
    }

    void Start()
    {
        EventsHandler.OnRockBrokenWithInfo += IncreaseScore;
    }

    public int RockScore(string rockTag)
    {
        int scoreIncrease;
        switch (rockTag)
        {
            case "Rock_Light":
                scoreIncrease = scoreLight;
                break;
            case "Rock_Medium":
                scoreIncrease = scoreMedium;
                break;
            case "Rock_Big":
                scoreIncrease = scoreBig;
                break;
            case "Rock_Giant":
                scoreIncrease = scoreGiant;
                break;
            default:
                throw new ArgumentException();
        }
        return scoreIncrease;
    }

    void IncreaseScore(Vector3 _, string rockTag)
    {
        score += RockScore(rockTag);
    }

    void OnDestroy()
    {
        EventsHandler.OnRockBrokenWithInfo -= IncreaseScore;
    }
}
