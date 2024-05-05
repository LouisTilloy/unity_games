using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DisplayTextWhenDestroyed : MonoBehaviour
{
    [SerializeField] float displayTime;
    [SerializeField] float fadeTime;
    [SerializeField] float fontSize;
    [SerializeField] Color color;
    [SerializeField] List<string> powerupPickupNames;
    ObjectPooling textObjectPooling;

    void Start()
    {
        textObjectPooling = GetComponent<ObjectPooling>();
        EventsHandler.OnPowerupGrabWithInfo += DisplayPowerupName;
    }

    void DisplayPowerupName(int powerupIndex, Vector3 hitPosition)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(hitPosition);

        // Get a pooled text object and modify its text and position.
        GameObject textObject = textObjectPooling.GetPooledObject();
        TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();
        textComponent.text = powerupPickupNames[powerupIndex];
        textComponent.fontSize = fontSize;
        textComponent.color = color;

        textObject.transform.position = screenPosition;

        // Display it for some time
        StartCoroutine(SharedUtils.DisplayAndFade(textObject, textComponent, displayTime, fadeTime));
    }


    private void OnDestroy()
    {
        EventsHandler.OnPowerupGrabWithInfo -= DisplayPowerupName;
    }
}
