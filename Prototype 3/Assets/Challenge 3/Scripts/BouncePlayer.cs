using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    public float bounceVelocityReduction;
    public float bounceForce;
    public AudioClip bounceSound;

    // Components
    private Rigidbody playerRigidbody;
    private AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerRigidbody.velocity = -playerRigidbody.velocity / bounceVelocityReduction;
            playerRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerAudioSource.PlayOneShot(bounceSound, 1.0f);
        }
    }
}
