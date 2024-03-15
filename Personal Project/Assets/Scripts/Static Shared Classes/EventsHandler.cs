using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsHandler : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public static event GeneralEventHandler OnGameOver;
    public static event GeneralEventHandler LateOnGameOver;
    public static event GeneralEventHandler OnLifeLost;
    public static event GeneralEventHandler OnProjectileFreeze;
    public static event GeneralEventHandler OnShieldCharged;
    public static event GeneralEventHandler OnShieldBroken;
    public static event GeneralEventHandler OnRockBroken;
    public static event GeneralEventHandler OnProjectileShot;
    public static event GeneralEventHandler OnScreenResolutionChange;

    public delegate void IntEventHandler(int powerupIndex);
    public static event IntEventHandler OnPowerupGrab;
    
    public delegate void ScoreEventHandler(Vector3 position, string rockType);
    public static event ScoreEventHandler OnRockBrokenWithInfo;

    public static void InvokeOnGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void InvokeLateOnGameOver()
    {
        LateOnGameOver?.Invoke();
    }

    public static void InvokeOnLifeLost()
    {
        OnLifeLost?.Invoke();
    }

    public static void InvokeOnProjectileFreeze()
    {
        OnProjectileFreeze?.Invoke();
    }

    public static void InvokeOnShieldCharged()
    {
        OnShieldCharged?.Invoke();
    }

    public static void InvokeOnShieldBroken()
    {
        OnShieldBroken?.Invoke();
    }

    public static void InvokeOnProjectileShot()
    {
        OnProjectileShot?.Invoke();
    }

    public static void InvokeOnPowerupGrab(int powerupIndex)
    {
        OnPowerupGrab?.Invoke(powerupIndex);
    }

    public static void InvokeOnScreenResolutionChange()
    {
        OnScreenResolutionChange?.Invoke();
    }

    public static void InvokeOnRockBroken(Vector3? hitPosition = null, string rockType = null)
    {
        if (hitPosition is not null && rockType is not null)
        {
            OnRockBrokenWithInfo?.Invoke((Vector3)hitPosition, rockType);
        }
        OnRockBroken?.Invoke();
    }
}
