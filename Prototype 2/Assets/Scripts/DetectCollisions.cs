using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public GameObject player;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void markCollision(Collider other)
    {
        if (other.gameObject.GetComponent<DetectCollisions>() != null)
        {
            collided = true;
            other.gameObject.GetComponent<DetectCollisions>().collided = true;
        } 
    }

    private bool IsProjectile(GameObject gameObject)
    {
        return gameObject.tag.Contains("Projectile");
    }

    private void DestroyIfPossible(GameObject gameObject)
    {
        if (!gameObject.CompareTag("Indestructible Projectile"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevents objects from colliding twice
        if (
            // collided ||
            (other.gameObject.GetComponent<DetectCollisions>() != null &&
             other.gameObject.GetComponent<DetectCollisions>().collided)
        )
        {
            return;
        }

        // Destroy object if it collides with another object. Except if:
        // - The other object is the Player
        // - The other object is a power up
        // - Both objects are projectiles
        if (
            !other.CompareTag("Player") && !other.CompareTag("Power Up") 
            && !(IsProjectile(gameObject) && IsProjectile(other.gameObject))
        )
        {
            DestroyIfPossible(gameObject);
            DestroyIfPossible(other.gameObject);
        }

        // Increase score by 1 when feeding a nice animal
        if (IsProjectile(other.gameObject) && CompareTag("Animal"))
        {
            player.GetComponent<PlayerController>().IncreaseScoreByOne();
            markCollision(other);
            
        }

        // Decrease lives by 1 when getting hit by aggressive animal
        if (other.CompareTag("Player") && CompareTag("Aggressive Animal"))
        {
            other.gameObject.GetComponent<PlayerController>().looseOneLife();
            markCollision(other);
        }
    }
}
