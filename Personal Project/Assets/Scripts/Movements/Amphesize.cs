using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amphesize : MonoBehaviour
{
    [SerializeField] float magnitude;
    [SerializeField] float speed;
    Vector3 InitialEulerAngles;

    void Awake()
    {
        InitialEulerAngles = transform.eulerAngles;
    }

    void Update()
    {
        transform.eulerAngles = InitialEulerAngles + 
            new Vector3(magnitude * Mathf.Cos(Time.time * speed), magnitude * Mathf.Sin(Time.time * speed), 0.0f);
    }
}
