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
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(hitPosition);
        // Vector3 viewportPos = Camera.main.WorldToViewportPoint(hitPosition);
        // Vector3 screenPosition = new Vector3(viewportPos.x * Screen.width, viewportPos.y * Screen.height, 0.0f);

        // Get a pooled text object and modify its text and position.
        GameObject textObject = textObjectPooling.GetPooledObject();
        TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = "+ " + score.ToString();
        textComponent.fontSize = fontSizes[rockIndex];
        textComponent.color = colors[rockIndex];
        textComponent.rectTransform.position = screenPosition;

        // Display it for some time
        StartCoroutine(SharedUtils.DisplayAndFade(textObject, textComponent, displayTime, fadeTime));
        // Make it jump
        StartCoroutine(ScoreJump(textComponent));
    }

    float JumpXtoY(float x)
    {
        return 4.0f * trajectoryHeight / Mathf.Pow(trajectoryLength, 2) * x * (trajectoryLength - x);
    }

    IEnumerator ScoreJump(TextMeshProUGUI textComponent)
    {
        float timer = 0;
        Vector3 textInitialPos = textComponent.rectTransform.position;
        float textXPos;
        float textYPos;
        while (timer < TotalDisplayTime())
        {
            textXPos = Mathf.Lerp(textInitialPos.x, textInitialPos.x + trajectoryLength, timer / TotalDisplayTime());
            textYPos = JumpXtoY(textXPos - textInitialPos.x) + textInitialPos.y;
            textComponent.rectTransform.position = new Vector3(textXPos, textYPos);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnRockBrokenWithInfo -= DisplayScore;
    }

}
