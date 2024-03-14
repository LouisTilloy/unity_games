using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public string enemyBaseTag = "Rock";

    [SerializeField] private float invicibilityDuration = 3.0f;

    [HideInInspector] public int maxLives = 3;
    [HideInInspector] public int lives;
    [HideInInspector] public bool isInvincible = false;
    [HideInInspector] public bool isShielded = false;

    private void Start()
    {
        lives = maxLives;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains(enemyBaseTag) && !isInvincible && !isShielded)
        {
            lives -= 1;
            EventsHandler.InvokeOnLifeLost();
            if (lives > 0)
            {
                StartCoroutine(SharedUtils.WaitThenPauseGameForSeconds(0.0f, 0.1f));
                StartCoroutine(TemporaryInvincibility(invicibilityDuration));
            }
            else if (lives == 0)
            {
                EventsHandler.InvokeOnGameOver();
            }
        }
    }

    public IEnumerator TemporaryInvincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

}
