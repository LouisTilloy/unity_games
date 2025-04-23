using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class DisplayCityValue : MonoBehaviour
{
    [SerializeField] CityData cityReference;
    GameObject textObject;
    TextMeshPro textMeshPro;
    Color defaultColor;
    Vector3 defaultTextScale;

    void Start()
    {
        textObject = transform.GetChild(0).gameObject;
        textMeshPro = textObject.GetComponent<TextMeshPro>();
        defaultColor = textMeshPro.color;
        defaultTextScale = textObject.transform.localScale;
    }

    void Update()
    {
        int value = cityReference.GetParameter(name);
        textMeshPro.text = value.ToString();
        if (cityReference.ParameterAtMax(name))
        {
            textMeshPro.color = Color.red;
            textObject.transform.localScale = defaultTextScale * 1.25f;
        }
        else
        {
            textMeshPro.color = defaultColor;
            textObject.transform.localScale = defaultTextScale;
        }
    }
}
