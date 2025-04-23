using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectCityOnClick : MonoBehaviour
{
    public bool mouseHovering = false;
    string managerTag = "CityFightManager";
    CityFightManager cityFightManager;
    CityData cityData;
    public bool selected = false;

    void Start()
    {
        cityFightManager = GameObject.FindGameObjectWithTag(managerTag).GetComponent<CityFightManager>();
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
                Debug.Log("cityFightManager.CityFight(GetComponent<CityData>());");
                cityFightManager.CityFight(GetComponent<CityData>());
            }
            selected = false;
        }
    }
}
