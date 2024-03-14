using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionChangeTrigger : MonoBehaviour
{
    float currentWidth;
    float currentHeight;

    void Start()
    {
        currentHeight = Screen.height;
        currentWidth = Screen.width;
    }

    void Update()
    {
        if (currentHeight != Screen.height || currentWidth != Screen.width)
        {
            currentWidth = Screen.width;
            currentHeight = Screen.height;

            EventsHandler.InvokeOnScreenResolutionChange();
        }
    }
}
