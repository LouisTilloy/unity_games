using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    Dictionary<int, int> powerupIndexToSlotIndex;
    float maxFontSize = 60.0f;

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
        powerupIndexToSlotIndex = new Dictionary<int, int>();

        EventsHandler.OnPowerupGrab += UpdateUI;
    }

    // We ignore the powerup index and update the entire UI instead, just to me more robust to bugs
    void UpdateUI(int dummyPowerupIndex)
    {
        int currentPowerupLevel;
        int powerupSlotIndex;
        for (int powerupIndex = 0; powerupIndex < powerupManager.powerupLevels.Length; powerupIndex++)
        {
            currentPowerupLevel = powerupManager.powerupLevels[powerupIndex];
            
            // Skip innactive powerups
            if (currentPowerupLevel == 0) 
            { 
                continue; 
            }

            // If powerup is already active, we retrieve its slot
            if (powerupIndexToSlotIndex.ContainsKey(powerupIndex))
            {
                powerupSlotIndex = powerupIndexToSlotIndex[powerupIndex];
            }

            // If it is a new powerup, we assign it to a slot and display the corresponding sprite
            else
            {
                powerupSlotIndex = SharedUtils.MaxDictDefault(powerupIndexToSlotIndex, -1) + 1;
                powerupIndexToSlotIndex[powerupIndex] = powerupSlotIndex;
                powerupSlots[powerupSlotIndex].SetActive(true);
                powerupSlotImages[powerupSlotIndex].sprite = powerupSprites[powerupIndex];
            }
            
            // We finally update the level of the powerup in the UI
            if (powerupManager.IsLevelMax(powerupIndex)) 
            {
                powerupTexts[powerupSlotIndex].fontSize = maxFontSize;
                powerupTexts[powerupSlotIndex].text = "max";
            }
            else
            {
                powerupTexts[powerupSlotIndex].text = currentPowerupLevel.ToString();
            }
        }
    }

    void OnDestroy()
    {
        EventsHandler.OnPowerupGrab -= UpdateUI;
    }


}
