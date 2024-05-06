using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTransitionManager : MonoBehaviour
{
    [SerializeField] GameObject levelText;
    [SerializeField] List<GameObject> levelBackgrounds;
    [SerializeField] float waitTime;
    [SerializeField] float displayTime;
    [SerializeField] float fadeTime;

    TextMeshProUGUI levelTextComponent;

    private void Awake()
    {
        levelTextComponent = levelText.GetComponent<TextMeshProUGUI>();
        EventsHandler.OnLevelTransition += LevelTransition;
    }

    void LevelTransition(int nextLevel)
    {
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
    }

    private void OnDestroy()
    {
        EventsHandler.OnLevelTransition -= LevelTransition;
    }
}
