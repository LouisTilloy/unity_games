using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnWall : MonoBehaviour
{
    public bool isScriptActive = true;

    private float wallMarginForceDirection = 1.1f;
    private GameObject[] walls; // walls[0]: left, walls[1]: right
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

        // If a rock happens to be past the walls (because spawned from a rock that didnt quite enter yet)
        // then we need to make sure it is being directed to the game area.
        if (walls[1].transform.position.x + wallMarginForceDirection < transform.position.x )
        {
            moveRightScript.horizontalSpeed = -Mathf.Abs(moveRightScript.horizontalSpeed);
        }
        else if (transform.position.x < walls[0].transform.position.x - wallMarginForceDirection)
        {
            moveRightScript.horizontalSpeed = Mathf.Abs(moveRightScript.horizontalSpeed);
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
