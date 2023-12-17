using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsHandler : MonoBehaviour
{
    // Define a delegate with a return type and parameters
    public delegate void generalEventHandler();

    // Declare events based on the generalEventHandler delegate
    public static event generalEventHandler bossInitialLanding;
    public static event generalEventHandler bossLanding;
    public static event generalEventHandler playerContact;
    public static event generalEventHandler playerLanding;

    public static void InvokeBossInitialLanding()
    {
        bossInitialLanding?.Invoke();
    }

    public static void InvokeBossLanding()
    {
        bossLanding?.Invoke();
    }

    public static void InvokePlayerContact()
    {
        playerContact?.Invoke();
    }

    public static void InvokePlayerLanding()
    {
        playerLanding?.Invoke();
    }
}
