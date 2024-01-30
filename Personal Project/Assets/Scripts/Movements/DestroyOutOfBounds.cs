using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public List<float> xBounds;
    public List<float> yBounds;

    private void Start()
    {
        if (xBounds.Count != 2)
        {
            throw new ArgumentException("Error with xBounds."); 
        }

        if (yBounds.Count != 2)
        {
            throw new ArgumentException("Error with yBounds.");
        }
    }
    void Update()
    {
        bool xOutOfBound = transform.position.x < xBounds[0] || transform.position.x > xBounds[1];
        bool yOutOfBound = transform.position.y < yBounds[0] || transform.position.y > yBounds[1];
        if (xOutOfBound || yOutOfBound)
        {
            Destroy(gameObject);
        }
    }
}
