using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // References
    private GameObject player;
    private Rigidbody playerRigidBody;
    private Rigidbody bossRigidBody;

    // Internal states
    private Vector3 playerLastVelocity;
    private Vector3 selfLastPos;
    private bool onGround = false;
    private bool neverTouchGroundBefore = true;

    // Parameters
    private float islandSize = 14;
    private float speed = 350;
    private float breakSpeed = 1200;
    private float timeLookAhead = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
        bossRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!onGround)
        {
            // Do nothing if not on the ground
            return;
        }

        Vector3 playerFuturePosition = EstimatePlayerPosition(timeLookAhead);
        // /*
        // prevents to aim for further than the the island (it doesn't do much honestly)
        if (player.transform.position.y <= 0)
        {
            playerFuturePosition = selfLastPos;  // go towards the middle if player is already dying
        }
        else if (playerFuturePosition.magnitude > islandSize)
        {
            playerFuturePosition = playerFuturePosition.normalized * (islandSize - 1);  // still follow the player but aim a bit more inwards
        }
        // */
        Vector3 lookDirection = (playerFuturePosition - transform.position).normalized;
        Vector3 rawForceVector = lookDirection * bossRigidBody.mass * Time.fixedDeltaTime;
        SharedUtils.ApplyEfficientForce(bossRigidBody, rawForceVector, speed, breakSpeed);

        if (player.transform.position.y > 0)
        {
            selfLastPos = transform.position;
        }
           
    }

    private Vector3 EstimatePlayerPosition(float time)
    {
        Vector3 playerAcceleration = (playerRigidBody.velocity - playerLastVelocity) / Time.fixedDeltaTime;
        playerLastVelocity = playerRigidBody.velocity;
        return player.transform.position
            + new Vector3(playerRigidBody.velocity.x, 0, playerRigidBody.velocity.z) * time
            + new Vector3(playerAcceleration.x, 0, playerAcceleration.z) * Mathf.Pow(time, 2) / 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Island")
        {
            onGround = true;
            if (neverTouchGroundBefore)
            {
                EventsHandler.InvokeBossInitialLanding();
                neverTouchGroundBefore = false;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Island")
        {
            onGround = false;
        }
    }
}
