using System;
using UnityEngine;

public class SplitOnProjectileCollision : MonoBehaviour
{
    bool triggered = false;
    [SerializeField]
    private GameObject[] ballPrefabs;
    
    private void OnTriggerEnter(Collider other)
    {
        // Only interact with projectiles
        if (!other.CompareTag("Projectile")) { return; }

        // Destroy rock if the collider is a projectile that has not touched another rock yet.
        int projectileTriggerCount = other.GetComponentInParent<NumberOfTriggers>().numberOfTriggers;
        if (!triggered && projectileTriggerCount == 0)
        {
            other.GetComponentInParent<NumberOfTriggers>().numberOfTriggers = 1;
            other.transform.parent.gameObject.SetActive(false);
            
            ReplaceCurrentWithNewPrefabs();
            triggered = true;

            EventsHandler.InvokeOnRockBroken(transform.position, tag);
        }
    }

    private void ReplaceCurrentWithNewPrefabs() 
    {
        int nextIndex = SharedUtils.RockNameToPrefabIndex(gameObject.tag) - 1;
        if (nextIndex >= 0)
        {
            // Instantiate 2 new rocks
            GameObject ballLeft = Instantiate(ballPrefabs[nextIndex]);
            GameObject ballRight = Instantiate(ballPrefabs[nextIndex]);

            // Make sure their speed vertical speed is 0
            Rigidbody leftRigidbody = ballLeft.GetComponent<Rigidbody>();
            leftRigidbody.velocity = new Vector3(leftRigidbody.velocity.x, 0.0f, 0.0f);
            Rigidbody rightRigidbody = ballLeft.GetComponent<Rigidbody>();
            rightRigidbody.velocity = new Vector3(rightRigidbody.velocity.x, 0.0f, 0.0f);

            // Set their position to where the previous rock was hit
            ballLeft.transform.position = transform.position;
            ballRight.transform.position = transform.position;

            // Make 1 rock go to the opposite direction as the other
            ballLeft.GetComponent<MoveRight>().horizontalSpeed *= -1;
        }
        Destroy(gameObject);
    }

}
