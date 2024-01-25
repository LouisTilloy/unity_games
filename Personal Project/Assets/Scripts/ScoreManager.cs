using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    private IEnumerator scoreCoroutine;

    private float currentTime;

    private void Start()
    {
        EventsHandler.LateOnGameOver += DeactivateScore;
        ActivateScore();
    }

    private IEnumerator IncreaseScore()
    {
        currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(currentTime).ToString();
        gameOverScoreText.text = scoreText.text;
    }

    private void ActivateScore()
    {
        scoreText.enabled = true;
        scoreCoroutine = IncreaseScore();
        StartCoroutine(scoreCoroutine);
    }

    private void DeactivateScore()
    {
        StopCoroutine(scoreCoroutine);
        scoreText.enabled = false;
    }

    private void OnDestroy()
    {
        EventsHandler.LateOnGameOver -= DeactivateScore;
    }
}
