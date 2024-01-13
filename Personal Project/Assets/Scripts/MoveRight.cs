using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float horizontalSpeed;
    public bool rotate;
    private Rigidbody objectRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRigidBody.velocity = new Vector3(horizontalSpeed, objectRigidBody.velocity.y, objectRigidBody.velocity.z);
        if (rotate)
        {
            objectRigidBody.angularVelocity = new Vector3(0, 0, -horizontalSpeed);
        }
    }

}
