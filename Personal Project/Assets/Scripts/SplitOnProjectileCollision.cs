using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class SplitOnProjectileCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ballPrefabs;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            ReplaceCurrentWithNewPrefabs();
            Destroy(other.gameObject);
        }
    }

    private int GetCurrentPrefabIndex()
    {
        switch (gameObject.tag) 
        {
            case "Rock_Light":
                return 0;
            case "Rock_Medium":
                return 1;
            case "Rock_Big":
                return 2;
            case "Rock_Giant":
                return 3;
        }
        throw new ArgumentException();
    }

    private void ReplaceCurrentWithNewPrefabs() 
    {
        int nextIndex = GetCurrentPrefabIndex() - 1;
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
