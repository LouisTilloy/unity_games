using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ExpandUp : MonoBehaviour
{
    [SerializeField]
    private float frameHeightIncrease;

    // Update is called once per frame
    void Update()
    {
        // Increase size of projectile by the same amount each frame
        transform.localScale += frameHeightIncrease * Time.deltaTime * Vector3.up;

        // Move projectile up so that the bottom does
        // - Note: It so happens that increasing the scale by 1 increases the overall height by 2,
        // - meaning that one can just translate by the same number to keep the bottom at the same spot
        transform.position += frameHeightIncrease * Time.deltaTime * Vector3.up;
    }
}
