using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 200;
    public GameObject player;
    private SlowDownCameraX slowDownScript;

    private void Start()
    {
        slowDownScript = GetComponentInChildren<SlowDownCameraX>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotation is forbidden when using boost
        if (!slowDownScript.isCatchingUp)
        {
            transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
        }
        

        transform.position = player.transform.position; // Move focal point with player

    }

}
