using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDriver1 : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0.0f, 4.3f, 0.41f);
    private Vector3 rotationOffset = new Vector3(12.217f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.LookAt(player.transform);
        // Follow the player rotations
        
        transform.rotation = player.transform.rotation * Quaternion.Euler(rotationOffset);

        // Offset the camera in front of the player
        // TODO: make the offset relative to the player somehow
        // angle = player.rotation.angle + rotationOffset.angle
        // camera position = player.transform.position + offset * (0, sin(angle), cos(angle))
        // float angle = transform.eulerAngles.x;
        // transform.position = player.transform.position + new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle)) * offset.magnitude;
        
        transform.position = player.transform.TransformPoint(offset);
    }
}
