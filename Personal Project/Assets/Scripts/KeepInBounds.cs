using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInBounds : MonoBehaviour
{
    private float horizontalBound = 8.75f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, transform.position.z);
        }
    }
}
