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
            GameObject ballLeft = Instantiate(ballPrefabs[nextIndex]);
            GameObject ballRight = Instantiate(ballPrefabs[nextIndex]);
            ballLeft.transform.position = transform.position;
            ballRight.transform.position = transform.position;
            ballLeft.GetComponent<MoveRight>().horizontalSpeed *= -1;
        }
        Destroy(gameObject);
    }

}
