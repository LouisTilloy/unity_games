using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnProjectileCollision : MonoBehaviour
{
    bool triggered;
    List<ObjectPooling> rocksPooling;

    void Awake()
    {
        rocksPooling = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().rocksPooling;
    }

    void OnEnable()
    {
        triggered = false;
    }

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

            // ReplaceCurrentWithNewPrefabs();
            StartCoroutine(ReplaceCurrentWithNewPrefabs());
            triggered = true;

            EventsHandler.InvokeOnRockBroken(transform.position, tag);
        }
    }
    IEnumerator ReplaceCurrentWithNewPrefabs()
    {
        int nextIndex = SharedUtils.RockNameToPrefabIndex(gameObject.tag) - 1;
        if (nextIndex >= 0)
        {
            for (int direction = -1; direction < 2; direction += 2)
            {
                // Get smaller rock from pool
                GameObject ball = rocksPooling[nextIndex].GetPooledObject();
                // Set its position to where the previous rock was hit
                ball.transform.position = transform.position;
                // Make sure its vertical speed is 0
                Rigidbody leftRigidbody = ball.GetComponent<Rigidbody>();
                leftRigidbody.velocity = Vector3.zero;
                // Make 1 rock go to the opposite direction as the other
                MoveRight moveRightScript = ball.GetComponent<MoveRight>();
                moveRightScript.horizontalSpeed = direction * Mathf.Abs(moveRightScript.horizontalSpeed);
                // Activate the rock
                ball.GetComponent<BounceOnWall>().isScriptActive = true;
                ball.SetActive(true);

                yield return new WaitForEndOfFrame();
            }
        }
        // Deactivate the rock that was hit
        gameObject.SetActive(false);
    }

    /*
    private void ReplaceCurrentWithNewPrefabs() 
    {
        int nextIndex = SharedUtils.RockNameToPrefabIndex(gameObject.tag) - 1;
        if (nextIndex >= 0)
        {
            for (int direction = -1; direction < 0; direction += 2) 
            {
                // Get smaller rock from pool
                GameObject ball = rocksPooling[nextIndex].GetPooledObject();
                // Set its position to where the previous rock was hit
                ball.transform.position = transform.position;
                // Make sure its vertical speed is 0
                Rigidbody leftRigidbody = ball.GetComponent<Rigidbody>();
                leftRigidbody.velocity = Vector3.zero;
                // Make 1 rock go to the opposite direction as the other
                MoveRight moveRightScript = ball.GetComponent<MoveRight>();
                moveRightScript.horizontalSpeed = direction * Mathf.Abs(moveRightScript.horizontalSpeed);
                // Activate the rock
                ball.SetActive(true);
            }
        }
        // Deactivate the rock that was hit
        gameObject.SetActive(false);
    }
    */
}
