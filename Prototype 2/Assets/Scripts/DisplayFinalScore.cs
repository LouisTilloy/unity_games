using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFinalScore : MonoBehaviour
{
    public GameObject player;
    private TMP_Text gui;
    // Start is called before the first frame update
    void Start()
    {
        gui = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController.IsDead())
        {
            gui.text = playerController.score.ToString();
        }
        else
        {
            gui.text = "";
        }

    }
}