using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySpeedAndRpm : MonoBehaviour
{
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] TextMeshProUGUI rpmText;
    private TextMeshProUGUI speedometerText;
    // Start is called before the first frame update
    void Start()
    {
        speedometerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerSpeed = Mathf.RoundToInt(playerRigidbody.velocity.magnitude * 3.6f);
        speedometerText.SetText(playerSpeed.ToString() + " km/h");
        rpmText.SetText(((playerSpeed % 30) * 40).ToString() + " rpm");
    }
}
