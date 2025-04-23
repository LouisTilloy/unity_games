using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTownManager : MonoBehaviour
{
    [SerializeField] CityData cityData;

    void PassOneTurn()
    {
        cityData.wheat -= cityData.nVillagers;
        cityData.gold -= cityData.nSoldiers;
    }
}
