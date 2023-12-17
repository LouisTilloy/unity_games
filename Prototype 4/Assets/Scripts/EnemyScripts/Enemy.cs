using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 100;
    private Rigidbody enemyRigidBody;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Like Update but with a fixed frame rate (use with forces)
    void FixedUpdate()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRigidBody.AddForce(lookDirection * speed * enemyRigidBody.mass * Time.deltaTime);
    }
}
