using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xDestroy = 12;
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x) > xDestroy)
        {
            Destroy(gameObject);
        }
    }
}
