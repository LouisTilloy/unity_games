using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement : MonoBehaviour
{
    public bool freezeHappened = false;

    Transform projectileHeadTransform;
    float freezeHeight;
    MoveProjectileUp moveUpScript;

    private void Awake()
    {
        projectileHeadTransform = transform.Find("Projectile_Head");
        moveUpScript = GetComponent<MoveProjectileUp>();
        freezeHeight = GetComponentInChildren<DeactivateOutOfBounds>().yBoundUp - 1f;
        enabled = false;
    }

    void OnEnable()
    {
        moveUpScript.enabled = true;
        freezeHappened = false;
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
