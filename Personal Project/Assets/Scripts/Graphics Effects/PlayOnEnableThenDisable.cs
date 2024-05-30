using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnableThenDisable : MonoBehaviour
{
    ParticleSystem rockBreakingParticles;

    void Awake()
    {
        rockBreakingParticles = GetComponent<ParticleSystem>();
        var main = rockBreakingParticles.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnEnable()
    {
        rockBreakingParticles.Play();
    }

    void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}
