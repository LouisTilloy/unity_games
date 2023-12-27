using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    void Update()
    {
        // Cheat kills 1 random enemy if stuck
        if (Input.GetKeyDown("k"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
