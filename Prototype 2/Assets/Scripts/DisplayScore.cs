using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public GameObject player;
    private TMP_Text gui;
    private Vector3 gameOverOffset = new Vector3(0.0f, 10.0f, 7.0f);
    // Start is called before the first frame update
    void Start()
    {
        gui = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (!playerController.IsDead()){
            gui.text = playerController.playerLivesText() + "\n" + playerController.playerScoreText();
        }
        else
        {
            gui.text = "";
        }
        
    }

}
