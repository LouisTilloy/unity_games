using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchChildOnHover : MonoBehaviour
{
    List<GameObject> children;
    List<Color> originalColors;
    [SerializeField] Color colorBeingSelected;

    void Awake()
    {
        children = new List<GameObject> { transform.GetChild(0).gameObject, transform.GetChild(1).gameObject };
        originalColors = new List<Color> { children[0].GetComponent<SpriteRenderer>().color, children[1].GetComponent<SpriteRenderer>().color };
    }

    void SwitchToChild(int index)
    {
        foreach (GameObject child in children)
        {
            child.SetActive(false);
        }
        children[index].SetActive(true);
    }

    void ResetColors()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].GetComponent<SpriteRenderer>().color = originalColors[i];
        }
    }

    private void OnMouseUp()
    {
        ResetColors();
    }

    private void OnMouseDrag()
    {
        foreach (GameObject child in children)
        {
            child.GetComponent<SpriteRenderer>().color = colorBeingSelected;
        }
    }



    private void OnEnable()
    {
        SwitchToChild(0);
        ResetColors();
    }

    void OnMouseEnter()
    {
        SwitchToChild(1);
    }

    void OnMouseExit()
    {
        SwitchToChild(0);
        ResetColors();
    }
}
