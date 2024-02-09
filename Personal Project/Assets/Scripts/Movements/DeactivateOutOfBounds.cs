using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeactivateOutOfBounds : MonoBehaviour
{
    public float yBoundUp;

    MoveProjectileUp moveUpScript;
    Transform projectileHead;

    void Start()
    {
        projectileHead = transform.Find("Projectile_Head");
        moveUpScript = GetComponent<MoveProjectileUp>();
    }

    void Update()
    {
        if (projectileHead.position.y > yBoundUp)
        {
            // moveUpScript.ResetProjectilePosition();
            gameObject.SetActive(false);
        }
    }
}
