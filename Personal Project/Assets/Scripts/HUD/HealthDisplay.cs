using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // Heart prefab
    public GameObject heartIndicatorPrefab;

    // UI parameters
    private Vector3 topLeftHeartZone;
    private int maxHeartPerLine = 3;
    private float relativeSpaceBetweenHearts = 0.2f;

    // Useful GameObjects
    [SerializeField] private LivesManager livesManager;
    private Image[] heartFills;
    private GameObject[] heartContainers;

    // Internal variables
    private float heartWidth;
    private float heartHeight;

    void Start()
    {
        // Get parameters
        Rect heartRect = heartIndicatorPrefab.GetComponent<RectTransform>().rect;
        heartWidth = heartRect.width;
        heartHeight = heartRect.height;

        // Set position of initial heart
        Rect canvasRect = GetComponent<RectTransform>().rect;
        float canvasWidth = canvasRect.width;
        float canvasHeight = canvasRect.height;
        // topLeftHeartZone = Vector3.zero;
        topLeftHeartZone = new Vector3(-canvasWidth / 2 + heartWidth / 2, canvasHeight / 2 - heartHeight / 2, 0);

        // Create heart fills and get references
        heartContainers = new GameObject[livesManager.maxLives];
        heartFills = new Image[livesManager.maxLives];
        for (int heartIndex = 0; heartIndex < livesManager.maxLives; heartIndex++)
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
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthBar();
    }

    void updateHealthBar()
    {
        for (int heartIndex = 0; heartIndex < livesManager.maxLives; heartIndex++)
        {
            if (heartIndex + 1 <= livesManager.lives)
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
