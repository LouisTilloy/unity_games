using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HungerBarDisplayer : MonoBehaviour
{
    private HungerManager hungerManager;
    public GameObject hungerBarPrefab;
    public GameObject instantiatedHungerBar;
    private Slider hungerBarDisplay;
    private float yOffset = 10.0f;
    private int lastHungerValue = 0;

    // Handling bar smoothness of update
    private float updateTime = 0.05f;
    private float currentTimeSinceUpdate = 100000.0f;

    // Start is called before the first frame update
    void Start()
    {
        instantiatedHungerBar = Instantiate(hungerBarPrefab);
        hungerBarDisplay = instantiatedHungerBar.GetComponent<Slider>();
        hungerManager = GetComponent<HungerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHungerBarValue();
    }
        
    private void LateUpdate()
    {
        UpdateHungerBarPosition();
    }

    void UpdateHungerBarValue()
    {
        // If the hunger value changed, reset the timer to fill the hunger bar
        if (lastHungerValue != hungerManager.hungerValue && currentTimeSinceUpdate >= updateTime)
        {
            currentTimeSinceUpdate = 0.0f;
        }

        // Reach new hunger value in a short time specified by updateTime to make things smooth
        int newHungerValue = hungerManager.hungerValue;
        hungerBarDisplay.value = Mathf.Lerp(
            (float)lastHungerValue / hungerManager.hungerLimit,
            (float)newHungerValue / hungerManager.hungerLimit,
            currentTimeSinceUpdate / updateTime
        );

        // Update time for hunger bar progress
        currentTimeSinceUpdate += Time.deltaTime;

        // Update last hunger value to be able to check for the next hunger value change
        if (currentTimeSinceUpdate >= updateTime)
        {
            lastHungerValue = newHungerValue;
        }
    }

    void UpdateHungerBarPosition()
    {
        instantiatedHungerBar.transform.position = transform.position + yOffset * Vector3.up;
    }
}
