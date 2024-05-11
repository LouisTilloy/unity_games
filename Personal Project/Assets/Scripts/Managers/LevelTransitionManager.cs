using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject levelText;
    [SerializeField] List<GameObject> levelBackgroundsTop;
    [SerializeField] List<GameObject> levelBackgroundsBottom;
    [SerializeField] List<int> startLevels;
    [SerializeField] float waitTime;
    [SerializeField] float displayTime;
    [SerializeField] float fadeTime;

    int currentBackgroundIndex = 0;
    TextMeshProUGUI levelTextComponent;

    private void Awake()
    {
        levelTextComponent = levelText.GetComponent<TextMeshProUGUI>();
        EventsHandler.OnLevelTransition += LevelTransition;
    }

    void LevelTransition(int nextLevel)
    {
        // Display level Text
        float waitTimeOverride;
        if (nextLevel == 1)
        {
            waitTimeOverride = 0.0f;
        }
        else
        {
            waitTimeOverride = waitTime;
        }
        levelTextComponent.text = $"Level {nextLevel}";
        StartCoroutine(SharedUtils.WaitDisplayAndFade(levelText, levelTextComponent, waitTimeOverride, displayTime, fadeTime));

        // Change background image
        if (currentBackgroundIndex - 1 < startLevels.Count && nextLevel >= startLevels[currentBackgroundIndex + 1])
        {
            levelBackgroundsTop[currentBackgroundIndex].SetActive(false);
            levelBackgroundsBottom[currentBackgroundIndex].SetActive(false);
            currentBackgroundIndex++;
            levelBackgroundsTop[currentBackgroundIndex].SetActive(true);
            levelBackgroundsBottom[currentBackgroundIndex].SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventsHandler.OnLevelTransition -= LevelTransition;
    }
}
