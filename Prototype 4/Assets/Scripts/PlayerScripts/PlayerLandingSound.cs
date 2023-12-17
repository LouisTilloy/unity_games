using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingSound : MonoBehaviour
{
    public AudioSource landingSound;
    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.playerLanding += landingSound.Play;
    }

    void OnDestroy()
    {
        EventsHandler.playerLanding -= landingSound.Play;
    }
}
