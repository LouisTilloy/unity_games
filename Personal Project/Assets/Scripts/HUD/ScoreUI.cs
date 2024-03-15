using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    TextMeshProUGUI scoreText;
    float textSize1080p = 90; // Resolution of reference: 1920p x 1080p

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreText.text = "Score: " + scoreManager.Score().ToString();
        scoreText.fontSize = textSize1080p * SharedUtils.AspectRatioScalingFactor();
    }
}
