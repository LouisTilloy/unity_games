using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject driverCamera;
    private const float cameraSwitchCooldown = 0.5f;
    private float cameraSwitchInput;
    private float lastCameraUpdate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.SetActive(true);
        driverCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update last camera update time
        lastCameraUpdate += Time.deltaTime;
        // Get player input
        cameraSwitchInput = Input.GetAxis("CameraSwitch" + playerNumber.ToString());
        // If jump button is pressed, switching camera views and preventing pushing the button again for 1 second
        if (cameraSwitchInput == 1 & lastCameraUpdate >= cameraSwitchCooldown)
        {
            // Reset camera update time
            lastCameraUpdate = 0.0f;

            // Switch active camera
            bool mainCameraActive = mainCamera.activeSelf;
            mainCamera.SetActive(!mainCameraActive);
            driverCamera.SetActive(mainCameraActive);
        }
    }
}
