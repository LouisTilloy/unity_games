using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLandingSound : MonoBehaviour
{
    public AudioSource landingSound;

    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.bossInitialLanding += playLandingSound;
        EventsHandler.bossLanding += playLandingSound;
    }

    private void playLandingSound()
    {
        landingSound.Play();
    }

    private void OnDestroy()
    {
        EventsHandler.bossInitialLanding -= playLandingSound;
        EventsHandler.bossLanding -= playLandingSound;
    }
}
