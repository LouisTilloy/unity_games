using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnGround : MonoBehaviour
{
    public float bounceStrength;
    private Rigidbody objectRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            objectRigidBody.AddForce(Vector3.up * bounceStrength, ForceMode.Impulse);
        }
    }
}
