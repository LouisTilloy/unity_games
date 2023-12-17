using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossPowerups : MonoBehaviour
{
    public string powerup;
    public GameObject bossKnockbackIndicator;
    public GameObject bossRocketsIndicator;
    // Start is called before the first frame update
    void Start()
    {
        switch (powerup)
        {
            case "knockback":
                BossKnockbackPowerup bossKnockbackPowerup = gameObject.GetComponent<BossKnockbackPowerup>();
                StartCoroutine(bossKnockbackPowerup.Initialize(bossKnockbackIndicator));
                break;
            case "rockets":
                BossRocketPowerup bossRocketPowerup = gameObject.GetComponent<BossRocketPowerup>();
                StartCoroutine(bossRocketPowerup.Initialize(bossRocketsIndicator));
                break;
        }
    }
}
