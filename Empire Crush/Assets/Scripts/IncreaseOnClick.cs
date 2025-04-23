using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


public class IncreaseOnClick : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] CityData cityReference;
    [SerializeField] string valueToIncrease;
    [SerializeField] int increaseAmount;
    public bool isActive;

    readonly Dictionary<string, string> resourcesToProfessionals = new()
    {
        ["wheat"] = "nFarmers",
        ["gold"] = "nMerchants",
        ["wood"] = "nLumberjacks",
        ["stone"] = "nMiners"
    };
    readonly Dictionary<string, int> professionalEfficieny = new()
    {
        ["nFarmers"] = 8,
        ["nMerchants"] = 6,
        ["nLumberjacks"] = 6,
        ["nMiners"] = 4,
    };

    void OnMouseUpAsButton()
    {
        if (isActive)
        {
            turnManager.ConsumeOneTurn();

            int newValue = cityReference.GetParameter(valueToIncrease) + GetIncreaseAmount();
            cityReference.SetParameter(valueToIncrease, newValue);
        }
    }

    int GetIncreaseAmount()
    {
        if (new List<string> {"nVillagers", "nSoldiers"}.Contains(valueToIncrease))
        {
            return 1;
        }
        string professional = resourcesToProfessionals[valueToIncrease];
        int nProfessionals = cityReference.GetParameter(professional);
        return nProfessionals * professionalEfficieny[professional] + 5;
    }
}
