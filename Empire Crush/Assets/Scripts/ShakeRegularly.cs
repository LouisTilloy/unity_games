using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeRegularly : MonoBehaviour
{
    float timeBetweenShakes = 3.0f;  // in seconds
    float shakeMagnitude = 1.0f;  // in degrees
    float shakeTime = 0.5f; // in seconds
    Vector3 defaultEulers;

    // Start is called before the first frame update
    void Start()
    {
        defaultEulers = gameObject.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        if(Time.time % timeBetweenShakes < shakeTime)
        {
            gameObject.transform.localRotation = Quaternion.Euler(defaultEulers[0], defaultEulers[1] + Random.Range(-shakeMagnitude, shakeMagnitude), defaultEulers[2]);
        }
        else
        {
            gameObject.transform.localRotation = Quaternion.Euler(defaultEulers);
        }
    }
}
