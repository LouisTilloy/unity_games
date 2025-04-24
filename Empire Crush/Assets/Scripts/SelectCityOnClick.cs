using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectCityOnClick : MonoBehaviour
{
    // Assigned in the Unity editor for the home town, loaded at runtime for the others.
    public AttackConfirm confirmScreenScript;
    // Data of the city attached to this object.
    CityData cityData;
    // Internal logic variables
    public bool mouseHovering = false;
    public bool selected = false;

    void Start()
    {
        cityData = GetComponent<CityData>();
    }

    void OnMouseOver()
    {
        mouseHovering = true;
    }

    void OnMouseExit()
    {
        mouseHovering = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !mouseHovering)
        {
            selected = false;
        }
    }

    private void OnMouseUp()
    {
        if (!selected)
        {
            selected = true;
        }
        else
        {   
            if (!cityData.isFriendly)
            {
                GameObject homeTown = GameObject.FindGameObjectWithTag("homeTown");
                CityData homeTownData = homeTown.GetComponent<CityData>();
                confirmScreenScript = homeTown.GetComponent<SelectCityOnClick>().confirmScreenScript;
                confirmScreenScript.SetActiveWithParams(homeTownData.nSoldiers, cityData.nSoldiers);
            }
            selected = false;
        }
    }
}
