using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueWhenFrozen : MonoBehaviour
{
    [SerializeField] List<Material> frozenColorMaterials;
    [SerializeField] List<Material> defaultMaterials;

    FreezeMovement freezeMovementScript;
    List<MeshRenderer> renderers;
    bool switchHappened = false;

    private void OnEnable()
    {
        int idx = 0;
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = defaultMaterials[idx];
            idx++;
        }
        switchHappened = false;
    }

    void Awake()
    {
        renderers = new List<MeshRenderer>();
        freezeMovementScript = GetComponent<FreezeMovement>();
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderers.Add(renderer);
        }
    }

    private void Update()
    {
        // Only do this once as soon as the projectile is frozen
        if (!switchHappened && freezeMovementScript.freezeHappened)
        {
            renderers[0].material = frozenColorMaterials[0];
            renderers[1].material = frozenColorMaterials[1];
            switchHappened = true;
        }
    }
}
