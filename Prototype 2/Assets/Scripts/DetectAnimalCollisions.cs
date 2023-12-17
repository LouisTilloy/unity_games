using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAnimalCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool IsAnimal(Collider other)
    {
        return other.tag.Contains("Animal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAnimal(other))
        {
            Destroy(gameObject);
        }
    }
}
