using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsHandler : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public static event GeneralEventHandler OnGameOver;
    public static event GeneralEventHandler LateOnGameOver;
    public static event GeneralEventHandler OnLifeLost;

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
}
