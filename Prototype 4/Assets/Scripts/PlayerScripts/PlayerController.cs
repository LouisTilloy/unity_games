using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    // ===== TODOs =====

    // --- ACTIVE ---
    // 2. The spinning of camera is confusing, try to find a way to make it less confusing

    // --- ARCHIVE ---
    // 1. Make movement more obvious [DONE]
    // Can add particle effects when using the top and bottom arrow (as if propelled by fire) [DONE]
    // 3. What character we are controlling is ambiguous [DONE]
    // Having a warm-up time where you can just move the ball around should help clarify controls [DONE]
    // 4. The goald of the game is ambiguous [DONE]
    // Adding a wave counter shoud help with this. [DONE]
    // Adding a text at the beginning does the job for now. [DONE]
    // It should be clearer the goal is to not fall from the platform. [DONE]
    // A warmup time to experiment with controls should also help [DONE]
    // 5. Make losing speed easier than gaining speed [DONE]
    // 6. Refactor code to make it less untangled (remove stuff from this file) [DONE]
    // 7. Game is too hard. Need to introduce lives. [DONE]
    // 8. Should add a game-over message [DONE]
    // 9. Implement lives instead of power-ups randomly [DONE]


    // Player setup
    private Rigidbody playerRigidBody;
    private GameObject focalPoint;
    public float speed;
    public float breakSpeed;

    // Internal states
    private bool onGround;

    // VFX effects
    // Jet Engine effect
    public ParticleSystem jetFireMovingForwards;
    public ParticleSystem jetFireMovingBackwards;

    // Sound effects
    // Jet sound
    public AudioSource jetAudio;
    private float jetMaxVolume = 0.1f;
    // Ball rolling
    public AudioSource ballRollingAudio;
    public AudioMixerGroup tempoChangeGroup;
    private float ballRollingMaxVolume = 0.07f;
    private float[] ballMinMaxTempo = { 0.5f, 1.0f};
    private float maxSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        
        jetAudio.volume = 0;
        jetAudio.Play();
        changeBallRollingTempo(0.5f);
        ballRollingAudio.outputAudioMixerGroup = tempoChangeGroup;
        ballRollingAudio.Play();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        jetFireMovingForwards.transform.position = transform.position - focalPoint.transform.forward * 0.8f;
        jetFireMovingForwards.transform.rotation = focalPoint.transform.rotation;
        jetFireMovingForwards.transform.Rotate(Vector3.up, 180.0f);
        
        jetFireMovingBackwards.transform.position = transform.position + focalPoint.transform.forward * 0.8f;
        jetFireMovingBackwards.transform.rotation = focalPoint.transform.rotation;
    }

    // Like Update but with a fixed frame rate (use with forces)
    void FixedUpdate()
    {
        float cameraForwardInput = Input.GetAxis("Vertical");
        
        // Jet audio
        jetAudio.volume = Mathf.Abs(cameraForwardInput) * jetMaxVolume;
        
        if (onGround)
        {
            // Ball rolling audio
            // - volume
            float percentageMaxSpeed = Mathf.Min(1.0f, playerRigidBody.velocity.magnitude / maxSpeed);
            ballRollingAudio.volume = percentageMaxSpeed * ballRollingMaxVolume;
            // - tempo
            float tempo = Mathf.Max(ballMinMaxTempo[0], ballMinMaxTempo[1] * percentageMaxSpeed);
            changeBallRollingTempo(tempo);
        }
        else
        {
            ballRollingAudio.volume = 0.0f;
        }

        
        // Jet fire animation
        if (cameraForwardInput > 0)
        {
            jetFireMovingForwards.Emit(5);
        }
        else if (cameraForwardInput < 0)
        {            
            jetFireMovingBackwards.Emit(5);
        }


        // Make it easier to break than to accelerate
        Vector3 rawForceVector = focalPoint.transform.forward * cameraForwardInput * Time.deltaTime;
        SharedUtils.ApplyEfficientForce(playerRigidBody, rawForceVector, speed, breakSpeed);
    }

    private void changeBallRollingTempo(float newTempo)
    {
        ballRollingAudio.pitch = newTempo;
        tempoChangeGroup.audioMixer.SetFloat("pitchBend", 1f / newTempo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EventsHandler.InvokePlayerContact();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Island")
        {
            onGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Island")
        {
            onGround = false;
        }   
    }
}
