using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveProjectileUp : MonoBehaviour
{
    private GameObject projectileBody;
    private GameObject projectileHead;
    private Vector3 headOffset;
    [SerializeField]
    private float frameHeightIncrease;
    [SerializeField]
    private float maximumHeight;

    private void Start()
    {
        headOffset = new Vector3(0.0f, -0.37f, -0.02f);
        projectileBody = transform.Find("Projectile_Body").gameObject;
        projectileHead = transform.Find("Projectile_Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase size of projectile by the same amount each frame
        projectileBody.transform.localScale += frameHeightIncrease * Time.deltaTime * Vector3.up;

        // Move projectile up so that the bottom does
        // - Note: It so happens that increasing the scale by 1 increases the overall height by 2,
        // - meaning that one can just translate by the same number to keep the bottom at the same spot
        projectileBody.transform.position += frameHeightIncrease * Time.deltaTime * Vector3.up;

        if (projectileBody.transform.position.y * 2 > maximumHeight)
        {
            Destroy(gameObject);
        }

        // Place head at the top of the body
        projectileHead.transform.position = ProjectileHeadPos(projectileBody.transform.position);
    }

    private Vector3 ProjectileHeadPos(Vector3 bodyPose)
    {
        return new Vector3(bodyPose.x, bodyPose.y * 2, bodyPose.z) + headOffset;
    }

}
