using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShakeObject : MonoBehaviour
{
    private float shakeCurrentDuration = 0;
    private float shakeMagnitude = 1f;
    private float shakeDuration = 0.25f;
    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        EventsHandler.bossInitialLanding += TriggerShakeWithPause;
        EventsHandler.bossLanding += TriggerShake;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeCurrentDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeCurrentDuration -= Time.unscaledDeltaTime;
        }
        else
        {
            transform.localPosition = initialPosition;
            initialPosition = transform.localPosition;
        }
    }

    public void TriggerShake()
    {
        shakeCurrentDuration = shakeDuration;
    }

    public void TriggerShakeWithPause()
    {
        shakeCurrentDuration = shakeDuration;
        StartCoroutine(SharedUtils.WaitThenPauseGameForSeconds(0, shakeDuration));
    }

    private void OnDestroy()
    {
        EventsHandler.bossInitialLanding -= TriggerShakeWithPause;
        EventsHandler.bossLanding -= TriggerShake;
    }
}