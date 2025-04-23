using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform fillTransform; // assign the "Fill" bar here
    SoldierData soldierData;
    SpriteRenderer spriteRenderer;
    float maxWidth;
    readonly float xPosOffset = 0.052f;

    void Start()
    {
        soldierData = GetComponentInParent<SoldierData>();
        spriteRenderer = fillTransform.GetComponent<SpriteRenderer>();
        maxWidth = fillTransform.localScale.x;
    }

    void Update()
    {
        SetHealthLevel(soldierData.currentHealth, soldierData.maxHealth);
        SetHealthColor(soldierData.currentHealth, soldierData.maxHealth);
    }

    void SetHealthLevel(float current, float max)
    {
        float ratio = Mathf.Clamp01(current / max);
        Vector3 scale = fillTransform.localScale;
        scale.x = ratio * maxWidth;
        fillTransform.localScale = scale;
        fillTransform.localPosition = new Vector3(xPosOffset + (ratio * maxWidth - maxWidth) / 2f, fillTransform.localPosition[1], fillTransform.localPosition[2]);
    }

    void SetHealthColor(float current, float max)
    {
        if (current > max / 2)
        {
            spriteRenderer.color = Color.Lerp(Color.yellow, Color.green, 2 * current / max - 1);
        }
        else
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.yellow, 2 * current / max);
        }
    }
}
