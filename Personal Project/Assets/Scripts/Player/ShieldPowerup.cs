using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    public float shieldRegenTimer;

    [SerializeField] List<float> shieldRegenMaxTimes;
    [SerializeField] float invincibilityDurationAfterBreak;
    PowerupManager powerupManager;
    LivesManager livesManager;
    bool shieldAlreadyBrokenOnce = false;

    
    public bool IsChargingActive()
    {
        return CurrentLevel() > 0 && shieldAlreadyBrokenOnce;
    }

    public float ChargeCompletion()
    {
        return Mathf.Min(shieldRegenTimer / TotalRegenTime(), 1);
    }

    void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
        livesManager = GetComponent<LivesManager>();
        shieldRegenTimer = 100.0f;  // To activate the powerup by default
    }

    int CurrentLevel()
    {
        return powerupManager.powerupLevels[2];
    }

    float TotalRegenTime()
    {
        return shieldRegenMaxTimes[CurrentLevel() - 1];
    }

    void Update()
    {
        if (CurrentLevel() == 0) 
        { 
            return; 
        }

        if (shieldRegenTimer >= TotalRegenTime() && !livesManager.isShielded)
        {
            livesManager.isShielded = true;
            EventsHandler.InvokeOnShieldCharged();
        }

        shieldRegenTimer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CurrentLevel() == 0)
        {
            return ;
        }

        if (other.tag.Contains(livesManager.enemyBaseTag) && shieldRegenTimer >= TotalRegenTime())
        {
            shieldRegenTimer = 0.0f;
            livesManager.isShielded = false;
            shieldAlreadyBrokenOnce = true;
            StartCoroutine(livesManager.TemporaryInvincibility(invincibilityDurationAfterBreak));

            EventsHandler.InvokeOnShieldBroken();
        }
    }

}
