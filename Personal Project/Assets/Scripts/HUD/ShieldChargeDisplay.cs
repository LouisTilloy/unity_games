using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShieldChargeDisplay : MonoBehaviour
{
    [SerializeField] ShieldPowerup shieldPowerupScript;
    [SerializeField] Color backgroundColor;
    [HideInInspector] public Image backgroundImage;

    public void setActive(Image background)
    {
        backgroundImage = background;
        backgroundImage.fillAmount = 0.0f;
        backgroundImage.color = backgroundColor;
        enabled = true;
    }

    void Update()
    {
        if (shieldPowerupScript.IsChargingActive()) 
        {
            backgroundImage.fillAmount = 1 - shieldPowerupScript.ChargeCompletion();
        }
    }
}
