using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    private bool isSpaceUsed = false;

    public float floatForce = 50.0f;
    public float pushDownForce;
    public float maxDownForce;
    private float upperBounceMargin = 1.0f;
    private float maxHeight = 15f;
    private float gravityModifier = 2f;
    
    private Rigidbody playerRb;
    private AudioSource playerAudio;

    public AudioClip moneySound;
    public AudioClip explodeSound;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;

        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);

    }

    // Update is called once per frame
    private void Update()
    {        
        if (Input.GetKey(KeyCode.Space))
        {
            isSpaceUsed = true;
        }
        else
        {
            isSpaceUsed = false;
        }

        // Player cannot go infinitely high
        KeepPlayerUnderHeight(maxHeight, upperBounceMargin);
    }

    void FixedUpdate()
    {
        // While space is pressed and player is low enough, float up
        if (isSpaceUsed && !gameOver && transform.position.y < maxHeight)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
    }
    private void OnCollisionEnter(Collision other)
    {

    }

    private void KeepPlayerUnderHeight(float height, float bounceMargin)
    {
        float deltaAboveHeight = transform.position.y - height;
        
        if (deltaAboveHeight > bounceMargin)
        {
            playerRb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, height + bounceMargin, transform.position.z);
        }
        if (deltaAboveHeight > 0)
        {
            playerRb.AddForce(Vector3.down * pushDownForce * deltaAboveHeight, ForceMode.Impulse);
        }
        
    }

}
