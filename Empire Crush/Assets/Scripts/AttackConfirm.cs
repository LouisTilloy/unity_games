using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackConfirm : MonoBehaviour
{
    public int nAttackers;
    public int nDefenders;
    [SerializeField] GameObject battleManager;
    [SerializeField] GameObject yesButton;
    [SerializeField] GameObject noButton;

    public void SetActiveWithParams(int nAttackers0, int nDefenders0)
    {
        nAttackers = nAttackers0;
        nDefenders = nDefenders0;
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == yesButton)
                {
                    OnYesPressed();
                }
                else if (hit.collider.gameObject == noButton)
                {
                    OnNoPressed();
                }
            }
        }
    }

    void OnYesPressed()
    {
        battleManager.transform.parent.gameObject.SetActive(true);
        battleManager.GetComponent<SpawnArmies>().Spawn(nAttackers, nDefenders);
        gameObject.SetActive(false);
    }

    void OnNoPressed()
    {
        gameObject.SetActive(false);
    }
}

