using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportTriggers : MonoBehaviour
{
    NumberOfTriggers totalNumberOfTriggers;

    public int GetCurrentTriggerCount()
    {
        return totalNumberOfTriggers.numberOfTriggers;
    }

    void Start()
    {
        totalNumberOfTriggers = GetComponentInParent<NumberOfTriggers>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Rock"))
        {
            totalNumberOfTriggers.numberOfTriggers += 1;
        }
    }
}
