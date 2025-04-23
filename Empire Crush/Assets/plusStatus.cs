using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class plusStatus : MonoBehaviour
{
    [SerializeField] CityData cityData;
    GameObject plusIcon;

    void Start()
    {
        plusIcon = transform.Find("plus_icon").gameObject;
        UpdatePlusIcon();
    }

    void UpdatePlusIcon()
    {
        if (cityData.nVillagers == 0)
        {
            plusIcon.SetActive(false);
        }
        else
        {
            plusIcon.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check every click how many villagers there are. Simpler than using events.
        if (Input.GetMouseButtonUp(0))
        {
            UpdatePlusIcon();
        }
    }
}
