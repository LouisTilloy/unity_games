using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayScoreIncrease : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] float displayTime;
    [SerializeField] float fadeTime;
    [SerializeField] List<float> fontSizes;
    [SerializeField] List<Color> colors;
    [SerializeField] float trajectoryLength;
    [SerializeField] float trajectoryHeight;
    ObjectPooling textObjectPooling;

    void Start()
    {
        textObjectPooling = GetComponent<ObjectPooling>();
        EventsHandler.OnRockBrokenWithInfo += DisplayScore;
    }

    float TotalDisplayTime()
    {
        return displayTime + fadeTime;
    }

    void DisplayScore(Vector3 hitPosition, string rockTag)
    {
        // Get score and screen position
        int score = scoreManager.RockScore(rockTag);
        int rockIndex = SharedUtils.RockNameToPrefabIndex(rockTag);
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, hitPosition);

        // Get a pooled text object and modify its text and position.
        GameObject textObject = textObjectPooling.GetPooledObject();
        TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();
        textComponent.alpha = 1.0f;
        textComponent.text = "+ " + score.ToString();
        textComponent.fontSize = fontSizes[rockIndex];
        textComponent.color = colors[rockIndex];
        textComponent.rectTransform.position = (Vector3)screenPosition;

        // Display it for some time
        StartCoroutine(DisplayAndFade(textObject, textComponent));
        // Make it jump
        StartCoroutine(ScoreJump(textComponent));
    }

    IEnumerator DisplayAndFade(GameObject textObject, TextMeshProUGUI textComponent)
    {
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

    float JumpXtoY(float x)
    {
        return 4.0f * trajectoryHeight / Mathf.Pow(trajectoryLength, 2) * x * (trajectoryLength - x);
    }

    IEnumerator ScoreJump(TextMeshProUGUI textComponent)
    {
        float timer = 0;
        Vector2 textInitialPos = textComponent.rectTransform.localPosition;
        float textXPos;
        float textYPos;
        while (timer < TotalDisplayTime())
        {
            textXPos = Mathf.Lerp(textInitialPos.x, textInitialPos.x + trajectoryLength, timer / TotalDisplayTime());
            textYPos = JumpXtoY(textXPos - textInitialPos.x);
            textComponent.rectTransform.localPosition = new Vector2(textXPos, textYPos);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnRockBrokenWithInfo -= DisplayScore;
    }

}
