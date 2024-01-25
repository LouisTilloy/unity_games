using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private float invicibilityDuration = 3.0f;

    [HideInInspector] public int maxLives = 3;
    [HideInInspector] public int lives;
    [HideInInspector] public bool isInvincible = false;
    private string enemyBaseTag = "Rock";

    private void Start()
    {
        lives = maxLives;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains(enemyBaseTag) && !isInvincible)
        {
            lives -= 1;
            if (lives > 0)
            {
                StartCoroutine(SharedUtils.WaitThenPauseGameForSeconds(0.0f, 0.1f));
                StartCoroutine(temporaryInvincibility());
            }
            else if (lives == 0)
            {
                EventsHandler.InvokeOnGameOver();
            }
        }
    }

    IEnumerator temporaryInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invicibilityDuration);
        isInvincible = false;
    }

}
