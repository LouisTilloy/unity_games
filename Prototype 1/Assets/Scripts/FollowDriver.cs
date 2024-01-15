using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDriver : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotationOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player rotations
        transform.rotation = player.transform.rotation * Quaternion.Euler(rotationOffset);

        // Offset the camera in front of the player
        transform.position = player.transform.TransformPoint(offset);
    }
}
