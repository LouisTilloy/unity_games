using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BreakRockParticlesSpawner : MonoBehaviour
{
    ObjectPooling breakRockParticlesPooling;
    [SerializeField] float speedIncreaseRate;
    [SerializeField] List<Material> materials;
    [SerializeField] List<float> radii;

    void Start()
    {
        breakRockParticlesPooling = GetComponent<ObjectPooling>();
        EventsHandler.OnRockBrokenWithInfo += spawnParticle;
    }

    void spawnParticle(Vector3 position, string rockType)
    {
        int rockIndex = SharedUtils.RockNameToPrefabIndex(rockType);
        GameObject particleSystemGameObject = breakRockParticlesPooling.GetPooledObject();

        // Set particles start position
        particleSystemGameObject.transform.position = position;

        // Set particles texture
        ParticleSystem rocksParticleSystem = particleSystemGameObject.GetComponent<ParticleSystem>();
        ParticleSystemRenderer renderer = rocksParticleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = materials[rockIndex];

        // Set radius of sphere from which particles are emited 
        var particleShape = rocksParticleSystem.shape;
        particleShape.radius = radii[rockIndex];

        // Set emission depending on radius
        var emission = rocksParticleSystem.emission;
        emission.rateOverTime = 250 * particleShape.radius;

        // Set speed depending on radius
        var main = rocksParticleSystem.main;
        main.startSpeed = 3.0f + (particleShape.radius - 0.4f) * speedIncreaseRate;

        particleSystemGameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventsHandler.OnRockBrokenWithInfo -= spawnParticle;
    }
}
