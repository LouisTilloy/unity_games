using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayScoreIncrease : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] float displayTime;
    ObjectPooling textObjectPooling;

    void Start()
    {
        textObjectPooling = GetComponent<ObjectPooling>();
        EventsHandler.OnRockBrokenWithInfo += DisplayScore;
    }

    void DisplayScore(Vector3 hitPosition, string rockTag)
    {
        // Get score and screen position
        int score = scoreManager.RockScore(rockTag);
        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, hitPosition);

        // Get a pooled text object and modify its text and position.
        GameObject textObject = textObjectPooling.GetPooledObject();
        TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = "+ " + score.ToString();
        textComponent.rectTransform.position = (Vector3)screenPosition;

        // Display it for some time
        StartCoroutine(SetActiveForTime(textObject, displayTime));
    }

    IEnumerator SetActiveForTime(GameObject textObject, float time)
    {
        textObject.SetActive(true);
        yield return new WaitForSeconds(time);
        textObject.SetActive(false);
    }

    void OnDestroy()
    {
        EventsHandler.OnRockBrokenWithInfo -= DisplayScore;
    }

}
