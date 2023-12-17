using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    public GameObject player;
    public HungerBarDisplayer hungerBarDisplayer; 
    public int hungerValue;
    public int hungerLimit;
    public float destroyObjectDelay;
    // Start is called before the first frame update
    void Start()
    {
        hungerValue = 0;
        destroyObjectDelay = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FeedAnimalOnce()
    {
        hungerValue += 1;
        // Debug.Log("hungerBarValue: " + hungerBarValue.ToString());
        if (hungerValue >= hungerLimit)
        {
            if (CompareTag("Animal"))
            {
                player.GetComponent<PlayerController>().IncreaseScoreByOne();
            }
            Destroy(gameObject, destroyObjectDelay);
            Destroy(GetComponent<HungerBarDisplayer>().instantiatedHungerBar, destroyObjectDelay);
        }
    }
}
