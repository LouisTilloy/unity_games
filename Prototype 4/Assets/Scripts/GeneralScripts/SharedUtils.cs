using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


readonly public struct SmashAttakParams
{
    readonly public float baseForceStrength;
    readonly public float forceDecreaseRate;
    readonly public float smashScaleRateWithMass;
    public SmashAttakParams(float baseForceStrength, float forceDecreaseRate, float smashScaleRateWithMass)
    {
        this.baseForceStrength = baseForceStrength;
        this.forceDecreaseRate = forceDecreaseRate;
        this.smashScaleRateWithMass = smashScaleRateWithMass;
    }
}

public class SharedUtils
{
    public static void ApplyJumpForce(Transform transform, string targetsTag, SmashAttakParams smashAttackParams)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetsTag);
        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceWithEnemy = Vector3.Distance(enemies[i].transform.position, transform.position);
            Vector3 forceDirection = (enemies[i].transform.position - transform.position).normalized;
            Rigidbody enemyRigidBody = enemies[i].GetComponent<Rigidbody>();
            float enemyMass = enemyRigidBody.mass;

            enemyRigidBody.AddForce(DistanceToSmashAttackForce(distanceWithEnemy, enemyMass, smashAttackParams) * forceDirection, ForceMode.Impulse);
        }
    }

    public static float DistanceToSmashAttackForce(float distance, float enemyMass, SmashAttakParams smashAttackParams)
    {
        float baseForceStrength = smashAttackParams.baseForceStrength;
        float forceDecreaseRate = smashAttackParams.forceDecreaseRate;
        float smashScaleRateWithMass = smashAttackParams.smashScaleRateWithMass;
        // Smash power scales with mass, this is why we multiply by a power of mass.
        return Mathf.Max(0, (baseForceStrength - distance * forceDecreaseRate) * Mathf.Pow(enemyMass, smashScaleRateWithMass));
    }

    public static IEnumerator JumpAnimation(Transform transform, float attackLoadTime, float maxYPos)
    {
        float time = 0;
        float initialYPos = transform.position.y;
        float yPos;
        while (time <= attackLoadTime)
        {
            yPos = JumpYTrajectory(initialYPos, time, attackLoadTime, maxYPos);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, initialYPos, transform.position.z);
    }

    private static float JumpYTrajectory(float initialPos, float time, float attackLoadTime, float maxYPos)
    {
        var a = 4.0f * (initialPos - maxYPos) / Mathf.Pow(attackLoadTime, 2);
        var b = 4.0f * (maxYPos - initialPos) / attackLoadTime;
        var c = initialPos;
        return a * Mathf.Pow(time, 2) + b * time + c;
    }

    public static IEnumerator WaitThenPauseGameForSeconds(float waitTime, float pauseTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        PauseGame();
        yield return new WaitForSecondsRealtime(pauseTime);
        UnPauseGame();
    }

    public static void PauseGame()
    {
       Time.timeScale = 0.0f;
    }

    public static void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }
    
    public static void ApplyEfficientForce(Rigidbody rigidbody, Vector3 rawForceVector, float speed, float breakSpeed)
    {
        Vector3 groundVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        Vector3 forwardDirection = groundVelocity.magnitude != 0 ? groundVelocity.normalized : Vector3.forward;
        Vector3 sideDirection = Vector3.Cross(Vector3.up, forwardDirection);

        float forwardForceMagnitude = Vector3.Dot(rawForceVector, forwardDirection);
        float sideForceMagnitude = Vector3.Dot(rawForceVector, sideDirection);

        float forwardSpeed = forwardForceMagnitude >= 0 ? speed : breakSpeed;
        rigidbody.AddForce(forwardForceMagnitude * forwardSpeed * forwardDirection);
        rigidbody.AddForce(sideForceMagnitude * speed * sideDirection);
    }

    public static void KnockbackCollide(GameObject strongActor, GameObject otherActor, float knockbackStrength, float knockbackScaleRateWithMass)
    {
        Rigidbody otherRigidBody = otherActor.GetComponent<Rigidbody>();
        Vector3 outwardsDirection = (otherRigidBody.transform.position - strongActor.transform.position).normalized;
        // PowerUp scales with mass, this is why we multiply by sqrt of mass.
        otherRigidBody.AddForce(outwardsDirection * knockbackStrength * Mathf.Pow(otherRigidBody.mass, knockbackScaleRateWithMass), ForceMode.Impulse);
    }

    public static GameObject[] RocketsSpawn(GameObject launcher, string targetTag, GameObject rocketPrefab, float rocketYSpawnPos)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject[] rockets = new GameObject[enemies.Length];
        Vector3 rocketSpawnPos;
        for (int i = 0; i < enemies.Length; i++)
        {
            rocketSpawnPos = new Vector3(launcher.transform.position.x, rocketYSpawnPos, launcher.transform.position.z);
            rockets[i] = Object.Instantiate(rocketPrefab, rocketSpawnPos, rocketPrefab.transform.rotation);
            rockets[i].GetComponent<ChaseEnemy>().chasedEnemyName = enemies[i].name;
        }
        return rockets;
    }

    public static IEnumerator MixAudio(
        AudioSource startAudio, 
        AudioSource targetAudio, 
        float exitTime, 
        float startTime, 
        float maxVolume
    )
    {
        float volume = startAudio.volume;
        while (volume > 0)
        {
            startAudio.volume = volume;
            volume -= Time.unscaledDeltaTime / exitTime;
            yield return null;
        }
        startAudio.Stop();

        volume = 0;

        targetAudio.Play();
        while (volume < maxVolume)
        {
            targetAudio.volume = volume;
            volume += Time.unscaledDeltaTime / startTime;
            yield return null;
        }
        targetAudio.volume = maxVolume;
    }

}
