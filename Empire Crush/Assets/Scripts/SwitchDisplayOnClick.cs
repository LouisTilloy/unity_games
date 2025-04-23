using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDisplayOnClick : MonoBehaviour
{
    [SerializeField] GameObject objectToDisplay;
    [SerializeField] GameObject objectToHide;

    private void OnMouseUpAsButton()
    {
        objectToDisplay.SetActive(true);
        objectToHide.SetActive(false);
    }
}
