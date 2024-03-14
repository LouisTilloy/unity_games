using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVisualizer : MonoBehaviour
{
    LivesManager livesManager;
    GameObject Shield;
    void Start()
    {
        livesManager = GetComponent<LivesManager>();
        Shield = transform.Find("Shield").gameObject;
    }

    void Update()
    {
        Shield.SetActive(livesManager.isShielded);
    }
}
