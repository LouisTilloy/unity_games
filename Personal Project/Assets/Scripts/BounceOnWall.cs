using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnWall : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private MoveRight moveRightScript;
    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        moveRightScript = GetComponent<MoveRight>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Wall"))
        {
            moveRightScript.horizontalSpeed *= -1;
        }
    }
}
