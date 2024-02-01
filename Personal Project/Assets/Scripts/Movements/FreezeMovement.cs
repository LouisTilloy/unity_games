using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement : MonoBehaviour
{
    Transform projectileHeadTransform;
    float freezeHeight;
    MoveProjectileUp moveUpScript;
    bool freezeHappened = false;

    void Start()
    {
        projectileHeadTransform = transform.Find("Projectile_Head");
        moveUpScript = GetComponent<MoveProjectileUp>();
        freezeHeight = GetComponentInChildren<DestroyOutOfBounds>().yBounds[1] - 1f;
    }

    void Update()
    {
        if (!freezeHappened && projectileHeadTransform.position.y > freezeHeight)
        {
            moveUpScript.enabled = false;
            EventsHandler.InvokeOnProjectileFreeze();
            freezeHappened = true;
        }
    }
}
