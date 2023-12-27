using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int targetPoints;
    [SerializeField]
    private ParticleSystem explosionParticles;

    private Rigidbody thisRigidbody;
    private GameManager gameManager;
    private float[] forceRange = { 9.0f, 15.0f };
    private float[] torqueRange = { -10.0f, 10.0f };
    private float[] xPosRange = { -4.0f, 4.0f };
    private float yPos = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        thisRigidbody.AddForce(randomRange(forceRange) * Vector3.up, ForceMode.Impulse);
        thisRigidbody.AddTorque(randomRange(torqueRange), randomRange(torqueRange), randomRange(torqueRange), ForceMode.Impulse);

        transform.position = new Vector3(randomRange(xPosRange), yPos);
    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive)
        {
            return;
        }

        Destroy(gameObject);
        gameManager.UpdateScore(targetPoints);
        Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
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
