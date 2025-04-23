using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

struct LogInfo
{
    public int wheatBefore;
    public int wheatAfter;
    public int goldBefore;
    public int goldAfter;

    public readonly int wheatLoss
    {
        get { return wheatBefore - wheatAfter ; }
    }
    public readonly int goldLoss
    {
        get { return goldBefore - goldAfter; }
    }
}

public class TurnManager : MonoBehaviour
{
    [SerializeField] TextMeshPro cityLogs;
    [SerializeField] CityData cityData;
    [SerializeField] List<IncreaseOnClick> resourcesButtons;
    [SerializeField] HomeTownManager homeTownManager;

    Queue<string> lastLogs;

    private void Start()
    {
        lastLogs = new Queue<string>();
    }

    private void Update()
    {
        // Check every click how many villagers there are. Simpler than using events.
        if (Input.GetMouseButtonUp(0))
        {
            if (cityData.nVillagers == 0 && cityData.nTurns > 0)
            {
                ActivateResourcesButtons();
            }
            else
            {
                DeactivateResourcesButtons();
            }
        }
    }

    void ActivateResourcesButtons()
    {
        foreach (IncreaseOnClick button in resourcesButtons)
        {
            button.isActive = true;
        }
    }

    void DeactivateResourcesButtons()
    {
        foreach (IncreaseOnClick button in resourcesButtons)
        {
            button.isActive = false;
        }
    }

    LogInfo UpdateResources()
    {
        int wheatBefore = cityData.wheat;
        int goldBefore = cityData.gold;
        cityData.wheat -= cityData.NumVillagers();
        cityData.gold -= cityData.nSoldiers;
        return new LogInfo() { wheatBefore=wheatBefore, wheatAfter=cityData.wheat, goldBefore=goldBefore, goldAfter=cityData.gold };
    }

    void DisplayLog(LogInfo info)
    {
        if (lastLogs.Count > 1)
        {
            lastLogs.Dequeue();
        }

        string newLog = $"\n   • {info.wheatLoss} wheat ({info.wheatBefore} -> {info.wheatAfter}) ," +
                        $"\n   • {info.goldLoss} gold ({info.goldBefore} -> {info.goldAfter})";
        lastLogs.Enqueue(newLog);

        cityLogs.text = $"Last turn, the city consumed:" +  string.Join("\nBefore last turn, the city consumed:", lastLogs.Reverse());
    }

    public void ConsumeOneTurn()
    {
        cityData.nTurns--;
        LogInfo logInfo = UpdateResources();
        DisplayLog(logInfo);
    }

    public void addTurns(int nTurnsAdded)
    {
        cityData.nTurns += nTurnsAdded;
    }
}
