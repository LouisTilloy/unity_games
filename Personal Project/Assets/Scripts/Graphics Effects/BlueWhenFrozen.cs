using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWhenFrozen : MonoBehaviour
{
    [SerializeField] Material frozenColorMaterial;
    List<Material> materials;

    void Start()
    {
        materials = new List<Material>();
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            materials.Add(renderer.material);
        }
        EventsHandler.OnProjectileFreeze += SwitchMaterialToFrozen;
    }

    void SwitchMaterialToFrozen()
    {
        materials[0].color = frozenColorMaterial.color;
        materials[1].color = frozenColorMaterial.color;
    }

    void OnDestroy()
    {
        EventsHandler.OnProjectileFreeze -= SwitchMaterialToFrozen;
    }
}
