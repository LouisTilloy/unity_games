using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private Slider volumeSlider;
    private float volumeReduction = 10.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused())
            {
                ResumeGame();
            }
            // Condition prevents pausing in main menu or game-over screen
            else if (gameManager.isGameActive)  
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        gameManager.isGameActive = false;
        pauseScreen.SetActive(true);
        volumeSlider.value /= volumeReduction;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        gameManager.isGameActive = true;
        pauseScreen.SetActive(false);
        volumeSlider.value *= volumeReduction;
    }

    bool isGamePaused() { 
        return Time.timeScale == 0; 
    }
}
