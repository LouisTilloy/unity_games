using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveProjectileUp : MonoBehaviour
{
    [SerializeField] private float frameHeightIncrease;
    [SerializeField] private float maximumHeight;

    private GameObject projectileBody;
    private GameObject projectileHead;
    private Vector3 headOffset;

    private float initialY = 0.8f;

    // Initial state
    private Vector3 initialHeadPosition;
    private Vector3 initialBodyPosition;
    private Vector3 initialBodyScale;

    private void Awake()
    {
        headOffset = new Vector3(0.0f, -0.31f, -0.02f);
        projectileHead = transform.Find("Projectile_Head").gameObject;
        projectileBody = transform.Find("Projectile_Body").gameObject;

        initialHeadPosition = projectileHead.transform.localPosition;
        initialBodyPosition = projectileBody.transform.localPosition;
        initialBodyScale = projectileBody.transform.localScale;
    }

    void OnEnable()
    {
        projectileHead.transform.localPosition = initialHeadPosition + initialY * Vector3.up;
        projectileBody.transform.localPosition = initialBodyPosition + initialY * Vector3.up;
        projectileBody.transform.localScale = initialBodyScale + initialY * Vector3.up;
    }

    void Update()
    {
        // Increase size of projectile by the same amount each frame
        projectileBody.transform.localScale += frameHeightIncrease * Time.deltaTime * Vector3.up;

        // Move projectile up so that the bottom stays at the same place
        // - Note: It so happens that increasing the scale by 1 increases the overall height by 2,
        // - meaning that one can just translate by the same number to keep the bottom at the same spot
        projectileBody.transform.localPosition += frameHeightIncrease * Time.deltaTime * Vector3.up;

        // Place head at the top of the body
        projectileHead.transform.localPosition = ProjectileHeadPos(projectileBody.transform.localPosition);
    }

    private Vector3 ProjectileHeadPos(Vector3 bodyPose)
    {
        return new Vector3(bodyPose.x, bodyPose.y * 2, bodyPose.z) + headOffset;
    }

}
