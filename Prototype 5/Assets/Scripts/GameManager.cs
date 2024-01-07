using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    private int lives;
    private float spawnRate = 1.0f;

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        // Store current volume
        AudioSource backgroundMusic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        GlobalMusicData.SetVolumeAndTime(backgroundMusic.volume, backgroundMusic.time);
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.SetActive(false);
    }
}
