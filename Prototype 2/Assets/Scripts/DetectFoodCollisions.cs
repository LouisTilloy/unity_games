using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to put on animals
public class DetectFoodCollisions : MonoBehaviour
{
    public GameObject player;
    public HungerManager hungerManager;
    public int hungerLimit;

    // Start is called before the first frame update
    void Start()
    {
        hungerManager = gameObject.GetComponent<HungerManager>();
        hungerManager.player = player;
    }
    
    private bool IsIndestructibleProjectile(GameObject gameObject)
    {
        return gameObject.CompareTag("Indestructible Projectile");
    }

    private bool IsProjectile(GameObject gameObject)
    {
        return gameObject.tag.Contains("Projectile");
    }

    private void OnTriggerEnter(Collider other)
    {

        // Increase score by 1 when filling up hunger bar of a nice animal
        if (IsProjectile(other.gameObject))
        {
            hungerManager.FeedAnimalOnce();
            if (IsIndestructibleProjectile(other.gameObject))
            {
                // Pizza is twice as nutritious as cookies obviously
                hungerManager.FeedAnimalOnce();
            }
        }

    }
}
