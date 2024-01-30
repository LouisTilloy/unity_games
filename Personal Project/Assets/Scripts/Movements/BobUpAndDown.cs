using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BobUpAndDown : MonoBehaviour
{
    [SerializeField]
    private float maxDistanceFromCenter = 0.5f;
    private float frequency = 2.0f;
    private float averageYPos;

    private void Start()
    {
        averageYPos = transform.position.y;
    }

    void Update()
    {
        float newYPos = Mathf.Sin(Time.timeSinceLevelLoad * frequency) * maxDistanceFromCenter + averageYPos;
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    }
}
