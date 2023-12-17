using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public GameObject player;
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float sideBound = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void destroyHungerBarIfExists()
    {
        HungerBarDisplayer hungerBarDisplayer = GetComponent<HungerBarDisplayer>();
        if (hungerBarDisplayer != null)
        {
            Destroy(hungerBarDisplayer.instantiatedHungerBar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy object if it goes out of player view.
        if (transform.position.z > topBound)
        {
            destroyHungerBarIfExists();
            Destroy(gameObject);
        }
        if (transform.position.z < lowerBound)
        {
            destroyHungerBarIfExists();
            Destroy(gameObject);
            if (CompareTag("Animal"))
            {
                player.GetComponent<PlayerController>().looseOneLife();
            }
        }
        if (transform.position.x < -sideBound || transform.position.x > sideBound)
        {
            destroyHungerBarIfExists();
            Destroy(gameObject);
        }
    }
}
