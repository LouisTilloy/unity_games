using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle2 : MonoBehaviour
{
    private float speed = 20.0f;
    private float playerStartMovingPosition = 90.0f;
    public GameObject player1;
    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If either player 1 or player 2 reach a specific z position, make the obstacle move
        bool player1Reach = player1.transform.position.z >= playerStartMovingPosition;
        bool player2Reach = player2.transform.position.z >= playerStartMovingPosition;
        int move = player1Reach || player2Reach ? 1 : 0;
        transform.Translate(Vector3.forward * Time.deltaTime * speed * move);
    }
}
