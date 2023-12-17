using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownCameraX : MonoBehaviour
{
    private float freezeTime = 0.15f;
    private float savedZPos;
    private bool wasZPosSaved;

    private Vector3 freezePos;

    private float catchUpTimer = 0;
    private float catchUpTime = 0.5f;
    public bool isCatchingUp = false;

    private Vector3 regularLocalPos;
    private Rigidbody cameraRigidBody;
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        cameraRigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        regularLocalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isCatchingUp)
        {
            StartCoroutine(CatchUpTimerCoroutine(catchUpTime));
            FreezePosition();
        }

        if (isCatchingUp)
        {
            if (catchUpTimer <= freezeTime)
            {
                KeepPosition();
            }
            else
            {
                CatchUp();
            }
        }
    }

    private IEnumerator CatchUpTimerCoroutine(float endTime)
    {
        isCatchingUp = true;

        catchUpTimer = 0;
        while (catchUpTimer <= endTime)
        {
            catchUpTimer += Time.deltaTime;
            yield return null;
        }

        isCatchingUp = false;
        wasZPosSaved = false;
        transform.localPosition = regularLocalPos;
    }

    private void FreezePosition()
    {
        freezePos = transform.position;
    }

    private void KeepPosition()
    {
        if (catchUpTimer <= freezeTime)
        {
            transform.position = freezePos;
        }
    }

    private void CatchUp()
    {
        if (!wasZPosSaved)
        {
            savedZPos = transform.localPosition.z;
            wasZPosSaved = true;
        }
        float zPos = Mathf.Lerp(savedZPos, regularLocalPos.z, (catchUpTimer - freezeTime) / catchUpTime);
        Debug.Log(zPos);
        transform.localPosition = new Vector3(regularLocalPos.x, regularLocalPos.y, zPos);
    }
}
