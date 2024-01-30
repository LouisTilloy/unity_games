using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPowerupsUI : MonoBehaviour
{
    [SerializeField] PowerupManager powerupManager;
    [SerializeField] List<Sprite> powerupSprites;

    List<GameObject> powerupSlots;
    List<Image> powerupSlotImages;
    List<TextMeshProUGUI> powerupTexts;

    void Start()
    {
        GameObject currentPowerupSlot;
        powerupSlots = new List<GameObject>();
        powerupSlotImages = new List<Image>();
        powerupTexts = new List<TextMeshProUGUI>();
        for (int slotIndex = 0; slotIndex < transform.childCount; slotIndex++)
        {
            currentPowerupSlot = transform.GetChild(slotIndex).gameObject;
            powerupSlots.Add(currentPowerupSlot);
            powerupSlotImages.Add(currentPowerupSlot.GetComponent<Image>());
            powerupTexts.Add(currentPowerupSlot.GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    void Update()
    {
        int currentPowerupLevel;
        for (int powerupIndex = 0; powerupIndex < powerupManager.powerupLevels.Length; powerupIndex++)
        {
            currentPowerupLevel = powerupManager.powerupLevels[powerupIndex];
            // If powerup is active, display its sprite and its level in the UI
            if (currentPowerupLevel >= 1)
            {
                powerupSlots[powerupIndex].SetActive(true);
                powerupSlotImages[powerupIndex].sprite = powerupSprites[powerupIndex];
                powerupTexts[powerupIndex].text = currentPowerupLevel.ToString();
            }
            // if powerup is not active, make sure it is deactivated
            else
            {
                powerupSlots[powerupIndex].SetActive(false);
            }
        }
    }
}
