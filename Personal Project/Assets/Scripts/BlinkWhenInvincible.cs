using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlinkWhenInvincible : MonoBehaviour
{
    LivesManager livesManager;
    [SerializeField] float flickeringPeriod;
    [SerializeField] float percentTimeNotRenderered;
    [SerializeField] GameObject playerSkin;

    private void Start()
    {
        livesManager = GetComponent<LivesManager>();
    }

    private void Update()
    {
        float cycleTime = (Time.time % flickeringPeriod) / flickeringPeriod;
        playerSkin.SetActive(!livesManager.isInvincible || cycleTime > percentTimeNotRenderered);
    }

    
}
