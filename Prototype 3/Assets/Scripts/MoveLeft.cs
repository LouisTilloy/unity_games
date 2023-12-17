using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float regularSpeed = 20;
    private PlayerController playerControllerScript;
    private float destroyPositionX = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver && playerControllerScript.gameStarted) 
        {
            transform.Translate(Vector3.left * Time.deltaTime * CurrentSpeed());
        }
        if (transform.position.x < destroyPositionX && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    float CurrentSpeed()
    {
        return playerControllerScript.dashing ? regularSpeed * playerControllerScript.dashMultiplier : regularSpeed;
    }
}
