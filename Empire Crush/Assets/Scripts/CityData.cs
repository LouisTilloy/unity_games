using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CityData : MonoBehaviour
{
    public bool isFriendly;
    public int nSoldiers;

    // == Only for friendly city ==
    // Resources
    public int wheat;
    public int wood;
    public int stone;
    public int gold;
    public int nVillagers;  // unassigned villagers

    // Storage per resource
    public int maxWheat;
    public int maxWood;
    public int maxStone;
    public int maxGold;

    // Villagers
    public int nMerchants;
    public int nLumberjacks;
    public int nMiners;
    public int nBuilders;
    public int nFarmers;

    // Others
    public int nTurns;

    public int NumVillagers()
    {
        return nVillagers + nMerchants + nLumberjacks + nMiners + nBuilders + nFarmers;
    }

    public int GetParameter(string name)
    {
        switch (name)
        {
            case "nSoldiers":
                return nSoldiers;
            case "wheat":
                return wheat;
            case "wood":
                return wood;
            case "gold":
                return gold;
            case "stone":
                return stone;
            case "villagers":
            case "nVillagers":
                return nVillagers;
            case "nMerchants":
                return nMerchants;
            case "nLumberjacks":
                return nLumberjacks;
            case "nMiners":
                return nMiners;
            case "nBuilders":
                return nBuilders;
            case "nFarmers":
                return nFarmers;
            case "nTurns":
                return nTurns;
            default:
                throw new ArgumentException(name);
        }
    }

    public void SetParameter(string name, int newValue)
    {
        switch (name)
        {
            case "nSoldiers":
                nSoldiers = newValue;
                break;
            case "wheat":
                wheat = Math.Min(newValue, maxWheat);
                break;
            case "wood":
                wood = Math.Min(newValue, maxWood);
                break;
            case "gold":
                gold = Math.Min(newValue, maxGold);
                break;
            case "stone":
                stone = Math.Min(newValue, maxStone);
                break;
            case "villagers":
            case "nVillagers":
                nVillagers = newValue;
                break;
            case "nMerchants":
                nMerchants = newValue;
                break;
            case "nLumberjacks":
                nLumberjacks = newValue;
                break;
            case "nMiners":
                nMiners = newValue;
                break;
            case "nBuilders":
                nBuilders = newValue;
                break;
            case "nFarmers":
                nFarmers = newValue;
                break;
            default:
                throw new ArgumentException();
        }
    }

    public bool ParameterAtMax(string parameter)
    {
        switch (parameter)
        {
            case "wheat":
                return wheat == maxWheat;
            case "wood":
                return wood == maxWood;
            case "gold":
                return gold == maxGold;
            case "stone":
                return stone == maxStone;
            default:
                return false;
        }
    }

    // TODO: fix this design a little ugly
    public string CityValueDisplayName(string cityValueName)
    {
        switch (cityValueName)
        {
            case "nSoldiers":
                return "Soldier";
            default:
                return "";
        }
    }

    // TODO: fix this design a little ugly
    public string TooltipName(string cityValueName)
    {
        if (cityValueName.StartsWith("n"))
        {
            return cityValueName.Substring(1);
        }
        else
        {
            return cityValueName;
        }
    }

}
