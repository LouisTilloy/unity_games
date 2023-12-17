using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHelper : MonoBehaviour
{
    public bool powerupActive;
    public GameObject powerIndicator;

    private IEnumerator removePowerupCoroutine;
    public GameObject actor;

    public void Start()
    {
        powerupActive = false;
    }

    public void UpdateIndicatorPosition(float yOffset)
    {
        powerIndicator.transform.position = actor.transform.position + new Vector3(0, yOffset, 0);
    }

    public void ResetCoroutine(float powerupTime)
    {
        if (removePowerupCoroutine != null)
        {
            StopCoroutine(removePowerupCoroutine);
        }
        powerupActive = true;
        powerIndicator.SetActive(true);
        removePowerupCoroutine = RemovePowerupAfterTime(powerupTime);
        StartCoroutine(removePowerupCoroutine);
    }

    public void HelperOnTriggerEnter(Collider other, string tag, float powerupTime)
    {
        if (!other.CompareTag(tag))
        {
            return;
        }
        Destroy(other.gameObject);
        ResetCoroutine(powerupTime);
    }

    private IEnumerator RemovePowerupAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        powerIndicator.SetActive(false);
        powerupActive = false;
    }
}
