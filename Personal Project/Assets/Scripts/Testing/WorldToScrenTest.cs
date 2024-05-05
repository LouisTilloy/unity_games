using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using System;
using UnityEditor;

public class WorldToScrenTest : MonoBehaviour
{
    public float speed;
    // [SerializeField] GameObject textObject;
    GameObject textObject;

    void Start()
    {
        textObject = new GameObject("text_text");
        textObject.transform.parent = GameObject.Find("Canvas").transform;
        TextMeshProUGUI textComponent = textObject.AddComponent<TextMeshProUGUI>();
        textComponent.fontSize = 40;
        textComponent.color = UnityEngine.Color.red;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.text = "TEXT";
    }

    void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        textObject.transform.position = screenPosition;
        
        if (Input.GetKey("q"))
        {
            transform.position += Time.deltaTime * speed * Vector3.left;
        }
        if (Input.GetKey("e"))
        {
            transform.position += Time.deltaTime * speed * Vector3.right;
        }
        if (Input.GetKey("up"))
        {
            transform.position += Time.deltaTime * speed * Vector3.up;
        }
        if (Input.GetKey("down"))
        {
            transform.position += Time.deltaTime * speed * Vector3.down;
        }
    }
}
