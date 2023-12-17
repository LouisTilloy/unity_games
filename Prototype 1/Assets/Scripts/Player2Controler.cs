using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controler : MonoBehaviour
{
    // Public variables
    public GameObject mainCamera;
    public GameObject driverCamera;

    // Private variables
    private float speed = 12.0f;
    private float turnSpeed = 30.0f;
    private float horizontalInput;
    private float verticalInput;
    private float cameraSwitchInput;
    private float cameraSwitchCooldown = 0.5f;
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
        horizontalInput = Input.GetAxis("Horizontal2");
        verticalInput = Input.GetAxis("Vertical2");
        cameraSwitchInput = Input.GetAxis("CameraSwitch2");

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


        // Things to try
        // 1) If we wanted to make the acceleration more realistic
        // the line below except there would be a max front speed and a max back speed
        // float actualSpeed = speed + verticalInput * 0.5f - speed * speed * 0.01f;
        // 2) The camera should follow the rotation, maybe with some latency
        // 3) Should check the vehicle is on the ground to go right/left


        // Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        // Turn the vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

    }
}
