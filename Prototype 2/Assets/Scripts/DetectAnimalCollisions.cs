using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAnimalCollisions : MonoBehaviour
{
    private bool IsAnimal(Collider other)
    {
        return other.tag.Contains("Animal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAnimal(other))
        {
            gameObject.SetActive(false);
        }
    }
}
