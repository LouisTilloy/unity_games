using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnWall : MonoBehaviour
{
    public bool isScriptActive = true;

    private GameObject[] walls;
    private Collider thisCollider;
    private MoveRight moveRightScript;
    void Start()
    {
        moveRightScript = GetComponent<MoveRight>();
        thisCollider = GetComponent<Collider>();
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    private void Update()
    {
        // Between 5 and 10, it will just keep the boolean assigned at initialization
        if (Mathf.Abs(transform.position.x) < 5)
        {
            isScriptActive = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isScriptActive) {
            Physics.IgnoreCollision(walls[0].GetComponent<Collider>(), thisCollider);
            Physics.IgnoreCollision(walls[1].GetComponent<Collider>(), thisCollider);
            return;
        }
        else
        {
            Physics.IgnoreCollision(walls[0].GetComponent<Collider>(), thisCollider, false);
            Physics.IgnoreCollision(walls[1].GetComponent<Collider>(), thisCollider, false);
        }

        if (collision.gameObject.name.Contains("Wall"))
        {
            moveRightScript.horizontalSpeed *= -1;
        }
    }

}
