using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTransitionManager : MonoBehaviour
{
    public GameObject levelText;
    [HideInInspector] public TextMeshProUGUI levelTextComponent;
    public float waitTime;
    public float displayTime;
    public float fadeTime;

    [SerializeField] GameObject shopBackgroundTop;
    [SerializeField] GameObject shopBackgroundBottom;
    [SerializeField] List<GameObject> levelBackgroundsTop;
    [SerializeField] List<GameObject> levelBackgroundsBottom;
    [SerializeField] List<int> startLevels;

    int currentBackgroundIndex = 0;
    
    float StartToFinishTextTime()
    {
        return waitTime + displayTime + fadeTime;
    }

    private void Awake()
    {
        levelTextComponent = levelText.GetComponent<TextMeshProUGUI>();
        EventsHandler.OnLevelTransition += LevelTransition;
    }

    public void LoadLevelBackground()
    {
        levelBackgroundsTop[currentBackgroundIndex].SetActive(true);
        levelBackgroundsBottom[currentBackgroundIndex].SetActive(true);
    }

    public void DeactivateLevelBackground()
    {
        levelBackgroundsTop[currentBackgroundIndex].SetActive(false);
        levelBackgroundsBottom[currentBackgroundIndex].SetActive(false);
    }

    public void LoadShopBackground()
    {
        shopBackgroundTop.SetActive(true);
        shopBackgroundBottom.SetActive(true);
    }

    public void DeactivateShopBackground()
    {
        shopBackgroundTop.SetActive(false);
        shopBackgroundBottom.SetActive(false);
    }

    void LevelTransition(int nextLevel)
    {
        // Shop => nextLevel = -1

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

        // Change background image
        if (nextLevel != -1 )
        {
            StartCoroutine(DisplayLevelText($"Level {nextLevel}", waitTimeOverride, displayTime, fadeTime));
            if (currentBackgroundIndex + 1 < startLevels.Count && nextLevel >= startLevels[currentBackgroundIndex + 1])
            {
                DeactivateLevelBackground();
                currentBackgroundIndex++;
            }
            DeactivateShopBackground();
            LoadLevelBackground();
        }
        else
        {
            StartCoroutine(DisplayLevelText("Shop", 0.0f, displayTime, fadeTime));
            DeactivateLevelBackground();
            LoadShopBackground();
            // Waiting for Shop to disappear before displaying Choose One.
            StartCoroutine(DisplayLevelText("Choose One", displayTime + fadeTime + 0.1f, displayTime, fadeTime));
        }
    }

    IEnumerator DisplayLevelText(string text, float waitTimeOverride, float displayTimeOverride, float fadeTimeOverride)
    {
        yield return new WaitForSeconds(waitTimeOverride);
        levelTextComponent.text = text;
        yield return SharedUtils.DisplayAndFade(levelText, levelTextComponent, displayTimeOverride, fadeTimeOverride);
    }

    private void OnDestroy()
    {
        EventsHandler.OnLevelTransition -= LevelTransition;
    }
}
