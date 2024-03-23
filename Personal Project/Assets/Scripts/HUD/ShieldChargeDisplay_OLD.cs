using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldChargeDisplay_OLD : MonoBehaviour
{
    [SerializeField] ShieldPowerup shieldPowerupScript;
    GameObject shieldChargeUI;
    Slider shieldChargeSlider;
    bool UIAlreadyActivated = false;
    
    void Start()
    {
        shieldChargeUI = transform.Find("ShieldChargeBar").gameObject;
        shieldChargeSlider = shieldChargeUI.GetComponent<Slider>();
    }

    void Update()
    {
        if (shieldPowerupScript.IsChargingActive())
        {
            if (!UIAlreadyActivated)
            {
                shieldChargeUI.SetActive(true);
                UIAlreadyActivated = true;
            }
            shieldChargeSlider.value = shieldPowerupScript.ChargeCompletion();
        }
    }
}
