using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAtGameStart : MonoBehaviour
{
    public float displayTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTimeUntilDesable());
    }

    IEnumerator WaitTimeUntilDesable()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        
        text.enabled = true;
        yield return new WaitForSeconds(displayTime);
        text.enabled = false;
    }
}
