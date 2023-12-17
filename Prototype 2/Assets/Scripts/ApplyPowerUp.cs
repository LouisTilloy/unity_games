using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyPowerUp : MonoBehaviour
{
    public string playerTag = "Player";
    public int powerUpIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(playerTag))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerController>().ApplyTemporaryPowerUp(powerUpIndex);
        }   
    }
}
