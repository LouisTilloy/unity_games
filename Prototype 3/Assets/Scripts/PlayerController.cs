using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public ParticleSystem jumpParticles;
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    private AudioSource playerAudio;
    
    private Vector3 originalGravity;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private float jumpForce = 1100;
    public float doubleJumpForce = 500;
    private float gravityMultiplier = 3;
    private float gravityMultiplierEndGame = 2.3f;
    public float dashMultiplier = 2.0f;
    public float dashScoreMultiplier = 3.0f;
    public float score = 0.0f;
    public float delayGameStart = 1.0f;

    public bool gameStarted = false;
    public bool isOnGround = true;
    private bool doubleJumped = false;
    public bool dashing = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        originalGravity = Physics.gravity;
        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        gameStarted = Time.time >= delayGameStart;
        if (gameOver || !gameStarted)
        {
            return;
        }

        MaybeJump();
        SetDashingState();
        IncreaseScore();
    }

    private void MaybeJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                isOnGround = false;
                dirtParticles.Stop();
                playerAnimator.SetTrigger("Jump_trig");
                JumpWithForce(jumpForce, ForceMode.Impulse);
            }
            else if (!doubleJumped)
            {
                doubleJumped = true;
                jumpParticles.Play();
                playerRigidbody.velocity = Vector3.zero;
                playerAnimator.SetTrigger("DoubleJump_trig");
                JumpWithForce(doubleJumpForce, ForceMode.VelocityChange);
            }
        }
    }

    private void SetDashingState()
    {
        dashing = Input.GetKey(KeyCode.LeftShift);
        if (dashing)
        {
            playerAnimator.SetFloat("Speed_f", 1.1f);
        }
        else
        {
            playerAnimator.SetFloat("Speed_f", 0.6f);
        }
    }

    private void IncreaseScore()
    {
        float frameScore = Time.deltaTime;
        if (dashing)
        {
            frameScore *= dashScoreMultiplier;
        }
        score += frameScore;
        Debug.Log(score);
    }

    private void JumpWithForce(float force, ForceMode forceMode)
    {
        playerRigidbody.AddForce(Vector3.up * force, forceMode);
        playerAudio.PlayOneShot(jumpSound, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameOver)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJumped = false;
            dirtParticles.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            Physics.gravity = originalGravity * gravityMultiplierEndGame;  // reset gravity to make obstacles fall nicely
            playerAnimator.SetInteger("DeathType_int", 1);
            playerAnimator.SetBool("Death_b", true);
            explosionParticles.Play();
            dirtParticles.Stop();
            playerAudio.PlayOneShot(crashSound, 2.0f);
        }
    }
}
