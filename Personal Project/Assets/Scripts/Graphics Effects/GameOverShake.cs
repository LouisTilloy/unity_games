using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverShake : MonoBehaviour
{
    private float shakeMagnitude = 0.25f;
    private float shakeDuration = 0.25f;
    private float pauseDuration = 2.5f;
    Vector3 initialPosition;

    void Start()
    {
        EventsHandler.OnGameOver += TriggerShakeWithPause;
        initialPosition = transform.localPosition;
    }

    IEnumerator ShakeBeforeFinalGameOver()
    {
        float shakeCurrentDuration = shakeDuration;
        while (shakeCurrentDuration > 0)
        {
            // Shake camera in random position to create a screen shaking effect
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeCurrentDuration -= Time.unscaledDeltaTime;
            yield return null;
        }
        // Reset camera position
        transform.localPosition = initialPosition;
        initialPosition = transform.localPosition;
    }

    IEnumerator PauseAndShake()
    {
        // Launch both coroutines
        Coroutine pauseCoroutine = StartCoroutine(SharedUtils.WaitThenPauseGameForSeconds(0, pauseDuration));
        Coroutine shakeCoroutine = StartCoroutine(ShakeBeforeFinalGameOver());

        // Wait for both coroutine to end
        yield return pauseCoroutine;
        yield return shakeCoroutine;

        // Proceed with the rest of the game-over steps (e.g. delete GameObjects, ...)
        EventsHandler.InvokeLateOnGameOver();
    }

    public void TriggerShakeWithPause()
    {
        StartCoroutine(PauseAndShake());
    }

    private void OnDestroy()
    {
        EventsHandler.OnGameOver -= TriggerShakeWithPause;
    }
}