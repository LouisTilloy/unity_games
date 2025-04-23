using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AssignVillager : MonoBehaviour
{
    [SerializeField] CityData cityData;

    private void OnEnable()
    {
        EventOnClick.OnClickControllerEvent += IncrementCount;
    }

    private void OnDisable()
    {
        EventOnClick.OnClickControllerEvent -= IncrementCount;
    }

    void IncrementCount(string jobClickedName)
    {
        if (jobClickedName == name && cityData.nVillagers > 0)
        {
            cityData.SetParameter(name, cityData.GetParameter(name) + 1);
            cityData.nVillagers -= 1;
        }
    }
}
