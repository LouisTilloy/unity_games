using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] float scaleMultiplier;
    [SerializeField] SelectCityOnClick cityClickStatus;
    [SerializeField] Color selectedColor;

    bool previousHoveringState;
    bool previousSelectedState;
    Color originalColor;
    Renderer objectRenderer;
    Outline outlineScript;
    Vector3 originalScale;

    void Start()
    {
        outlineScript = GetComponent<Outline>();
        originalScale = transform.localScale;
        outlineScript.enabled = false;

        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    SelectCityOnClick GetCity()
    {
        if (cityClickStatus == null)
        {
            cityClickStatus = GetComponent<SelectCityOnClick>();
        }
        return cityClickStatus;
    }

    void Update()
    {
        if (previousHoveringState != GetCity().mouseHovering || previousSelectedState != GetCity().selected)
        {
            UpdateHoveringState();
        }
    }

    void UpdateHoveringState()
    {
        if (GetCity().mouseHovering)
        {
            ActivateHighlight();
        }
        if (GetCity().selected)
        {
            ActivateHighlight();
            objectRenderer.material.color = selectedColor;
        }
        if (!GetCity().selected)
        {
            if (!GetCity().mouseHovering)
            {
                objectRenderer.material.color = originalColor;
                DeactivateHighlight();
            }
        }
        previousHoveringState = GetCity().mouseHovering;
        previousSelectedState = GetCity().selected;
    }

    void DeactivateHighlight()
    {
        outlineScript.enabled = false;
        gameObject.transform.localScale = originalScale;
    }

    void ActivateHighlight()
    {
        gameObject.transform.localScale = originalScale * scaleMultiplier;
        outlineScript.enabled = true;
    }
}
