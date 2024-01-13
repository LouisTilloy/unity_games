using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollisions : MonoBehaviour
{
    private string playerTag = "Player";

    private void HitAggressiveAnimal()
    {
        // Get player object
        GameObject player = GameObject.FindGameObjectsWithTag(playerTag)[0];
        
        // Loose one life
        player.gameObject.GetComponent<PlayerController>().looseOneLife();

        // Destroy the animal and its hunger bar with some delay
        HungerManager hungerManager = GetComponent<HungerManager>();
        HungerBarDisplayer hungerBarDisplayer = GetComponent<HungerBarDisplayer>();
        Destroy(gameObject, hungerManager.destroyObjectDelay);
        Destroy(hungerBarDisplayer.instantiatedHungerBar, hungerManager.destroyObjectDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Decrease lives by 1 when getting hit by aggressive animal
        if (other.CompareTag("Player"))
        {
            HitAggressiveAnimal();
        }
    }
}
