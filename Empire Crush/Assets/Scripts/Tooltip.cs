using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] CityData cityReference;
    [SerializeField] Vector3 tooltipOffset;
    [SerializeField] string cityDataValueToDisplay;
    [SerializeField] string tooltipStaticContent;

    TextMeshProUGUI tooltipTitleObject;
    TextMeshProUGUI tooltipContentObject;
    GameObject tooltipRender;
    GameObject tooltipLayout;
    CityData cityData;
    string tooltipTag = "tooltip";
    

    void Start()
    {
        // Allows to get a reference to the tooltip which is deactivated by default
        tooltipLayout = GameObject.FindGameObjectWithTag(tooltipTag);
        tooltipRender = tooltipLayout.transform.GetChild(0).gameObject;
        tooltipTitleObject = tooltipRender.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tooltipContentObject = tooltipRender.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        cityData = cityReference.GetComponent<CityData>();
    }

    void UpdateTooltip()
    {
        tooltipLayout.transform.position = gameObject.transform.position + tooltipOffset;
        tooltipTitleObject.text = cityData.TooltipName(gameObject.name);
        if (cityDataValueToDisplay != "")
        {
            int value = cityData.GetParameter(cityDataValueToDisplay);
            string name = cityData.TooltipName(cityDataValueToDisplay);
            string pluralMark = (value <= 1) || name.EndsWith("s") ? "" : "s";
            tooltipContentObject.text = $"{value} {name}{pluralMark}";
        }
        else
        {
            tooltipContentObject.text = tooltipStaticContent;
        }
    }


    private void OnMouseOver()
    {
        if (tooltipRender != null)
        {
            tooltipRender.SetActive(true);
            UpdateTooltip();
        }
    }

    private void OnMouseExit()
    {
        if (tooltipRender != null)
        {
            tooltipRender.SetActive(false);
        }
    }
}
