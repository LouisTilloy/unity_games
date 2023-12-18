using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    private float[] forceRange = { 9.0f, 15.0f };
    private float[] torqueRange = { -10.0f, 10.0f };
    private float[] xPosRange = { -4.0f, 4.0f };
    private float yPos = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();

        thisRigidbody.AddForce(randomRange(forceRange) * Vector3.up, ForceMode.Impulse);
        thisRigidbody.AddTorque(randomRange(torqueRange), randomRange(torqueRange), randomRange(torqueRange), ForceMode.Impulse);

        transform.position = new Vector3(randomRange(xPosRange), yPos);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    float randomRange(float[] range)
    {
        return Random.Range(range[0], range[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
