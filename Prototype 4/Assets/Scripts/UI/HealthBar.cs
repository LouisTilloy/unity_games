using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Heart prefab
    public GameObject heartIndicatorPrefab;

    // UI parameters
    private Vector3 topLeftHeartZone;
    private int maxHeartPerLine = 10;
    private int[] resolution = { 1920, 1080 };
    private float relativeSpaceBetweenHearts = 0.2f;

    // Useful GameObjects
    private ManageLivesAndGameOver livesManager;
    private GameObject canvas;
    private Image[] heartFills;
    private GameObject[] heartContainers;

    // Internal variables
    private float heartWidth;
    private float heartHeight;
    private int numberOfHearts;
    private int previousLives;
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to useful objects
        GameObject player = GameObject.Find("Player");
        livesManager = player.GetComponent<ManageLivesAndGameOver>();
        canvas = GameObject.Find("Canvas");

        // Get parameters
        numberOfHearts = livesManager.maxLives;
        heartWidth = heartIndicatorPrefab.GetComponent<RectTransform>().rect.width;
        heartHeight = heartIndicatorPrefab.GetComponent<RectTransform>().rect.height;

        // Set psotion of initial heart
        topLeftHeartZone = new Vector3(-resolution[0] / 2 + heartWidth, resolution[1] / 2 - heartWidth, 0);

        // Create heart fills and get references
        heartContainers = new GameObject[numberOfHearts];
        heartFills = new Image[numberOfHearts];
        for (int heartIndex = 0; heartIndex < numberOfHearts; heartIndex++)
        {
            heartContainers[heartIndex] = Instantiate(
                heartIndicatorPrefab, 
                Vector3.zero, 
                transform.rotation,
                transform
            );
            heartContainers[heartIndex].transform.localPosition = topLeftHeartZone
                + Vector3.right * (heartWidth * (1 + relativeSpaceBetweenHearts)) * (heartIndex % maxHeartPerLine)
                - Vector3.up * (int)(heartIndex / maxHeartPerLine) * (heartHeight * (1 + relativeSpaceBetweenHearts));

            // We want the image of the child (heartFill) and not the image of the gameObject directly (heartContainer)
            heartFills[heartIndex] = heartContainers[heartIndex].transform.GetChild(0).GetComponent<Image>();
            heartFills[heartIndex].fillAmount = 1;
        }

        // Initialize lives
        previousLives = livesManager.lives;
        currentLives = livesManager.lives;
    }

    // Update is called once per frame
    void Update()
    {
        currentLives = livesManager.lives;
        if (previousLives != currentLives)
        {
            updateHealthBar();
        }
        previousLives = currentLives;
    }

    void updateHealthBar()
    {
        for (int heartIndex = 0; heartIndex < numberOfHearts; heartIndex++)
        {
            if (heartIndex + 1 <= currentLives)
            {
                heartFills[heartIndex].fillAmount = 1;
            }
            else
            {
                heartFills[heartIndex].fillAmount = 0;
            }
        }
    }
}
