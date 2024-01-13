using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDriver1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0.0f, 4.3f, 0.41f);
    private Vector3 rotationOffset = new Vector3(12.217f, 0.0f, 0.0f);
    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player rotations
        transform.rotation = player.transform.rotation * Quaternion.Euler(rotationOffset);

        // Offset the camera in front of the player
        transform.position = player.transform.TransformPoint(offset);
    }
}
