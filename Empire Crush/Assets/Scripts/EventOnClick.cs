using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnClick : MonoBehaviour
{
    public static event Action<string> OnClickControllerEvent;

    private void OnMouseUpAsButton()
    {
        OnClickControllerEvent?.Invoke(transform.parent.name);
    }
}
