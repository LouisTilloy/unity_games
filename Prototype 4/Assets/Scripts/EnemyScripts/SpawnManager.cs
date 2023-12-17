using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    // References to objects
    // - Prefabs
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public GameObject[] powerupPrefabs;
    // - UI and music
    public TextMeshProUGUI waveIndicator;
    private MusicSwitch musicSwitch;
    
    // - Boss Powerup indicators
    public GameObject bossKnockbackIndicator;
    public GameObject bossRocketsIndicator;
    public GameObject bossSmashAttackIndicator;

    // Static parameters
    // - General
    private int warmupTime = 10;
    private float spawnRange = 10;
    private float enemySpawnYPos = 0.1f;
    private float powerupSpawnYPos = 0.15f;
    // - Bosses
    private float bossSpawnYpos = 20;
    private float bossDropInitialVelocity = 5;
    private float bossInitialScale = 2.0f;
    private float bossSizeScaling = 0.25f;
    private float bossMassScaling = 2;
    private int waveIntervalSpawnBoss = 3;

    // Dynamic internal states
    private int waveNumber = 0;
    public int enemyCounter = 0;
    private int bossWaveNumber = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        musicSwitch = GameObject.Find("Main Camera").GetComponent<MusicSwitch>();
        StartCoroutine(SpawnEnemiesGameLoop());
    }

    // Update is called once per frame
    void Update()
    {
        waveIndicator.text = "Wave #" + waveNumber.ToString();
    }

    IEnumerator SpawnEnemiesGameLoop()
    {
        yield return new WaitForSeconds(warmupTime);
        while (true)
        {
            enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCounter == 0)
            {
                LaunchWave();
            }
            yield return null;
        }
    }

    private void LaunchWave()
    {
        if (waveNumber > 0 && waveNumber % waveIntervalSpawnBoss == 0 && waveNumber / waveIntervalSpawnBoss > bossWaveNumber)
        {
            bossWaveNumber++;
            SpawnBoss(bossWaveNumber);
            musicSwitch.transitionMusic();
        }
        else
        {
            waveNumber++;
            if (waveNumber > waveIntervalSpawnBoss && waveNumber % waveIntervalSpawnBoss == 1)
            {
                musicSwitch.transitionMusic();  // change music after bossWave
            }
            SpawnEnemyWave(waveNumber, waveNumber <= 2);  // force 1st and 2nd wave to only have weak enemies
        }
        SpawnRandomPowerUp();
    }

    private GameObject getRandomEnemy(bool spawnOnlyRegularEnemies)
    {
        int prefabIndex;
        if (!spawnOnlyRegularEnemies)
        {
            prefabIndex = Random.Range(0, enemyPrefabs.Length);
        }
        else
        {
            prefabIndex = 0;
        }
        return enemyPrefabs[prefabIndex];
    }

    void SpawnBoss(int bossWaveNumber)
    {
        GameObject boss = Instantiate(bossPrefab, GetRandomPositionOnIsland(bossSpawnYpos), bossPrefab.transform.rotation);
        float bossIndex = (bossWaveNumber - waveIntervalSpawnBoss) / waveIntervalSpawnBoss;
        boss.transform.localScale = Vector3.one * (bossInitialScale + bossSizeScaling * bossIndex);

        Rigidbody bossRigidBody = boss.GetComponent<Rigidbody>();
        bossRigidBody.mass += bossIndex * bossMassScaling;
        bossRigidBody.velocity = Vector3.down * bossDropInitialVelocity;

        boss.name = "boss_wave_" + bossWaveNumber;

        BossKnockbackPowerup bossKnockbackPowerup;
        BossRocketPowerup bossRocketPowerup;
        BossSmashAttackPowerup bossSmashAttackPowerup;
        switch (bossWaveNumber)
        {
            case 1:
                break;
            case 2:
                bossKnockbackPowerup = boss.GetComponent<BossKnockbackPowerup>();
                StartCoroutine(bossKnockbackPowerup.Initialize(bossKnockbackIndicator));
                break;
            case 3:
                bossRocketPowerup = boss.GetComponent<BossRocketPowerup>();
                StartCoroutine(bossRocketPowerup.Initialize(bossRocketsIndicator));
                break;
            case 4:
                bossSmashAttackPowerup = boss.GetComponent<BossSmashAttackPowerup>();
                StartCoroutine(bossSmashAttackPowerup.Initialize(bossSmashAttackIndicator));
                break;
            default:
                bossKnockbackPowerup = boss.GetComponent<BossKnockbackPowerup>();
                StartCoroutine(bossKnockbackPowerup.Initialize(bossKnockbackIndicator));
                bossRocketPowerup = boss.GetComponent<BossRocketPowerup>();
                StartCoroutine(bossRocketPowerup.Initialize(bossRocketsIndicator));
                bossSmashAttackPowerup = boss.GetComponent<BossSmashAttackPowerup>();
                StartCoroutine(bossSmashAttackPowerup.Initialize(bossSmashAttackIndicator));
                break;
        }
    }

    void SpawnEnemyWave(int waveNumber, bool spawnOnlyRegularEnemies)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            GameObject enemyPrefab = getRandomEnemy(spawnOnlyRegularEnemies);
            GameObject enemy = Instantiate(enemyPrefab, GetRandomPositionOnIsland(enemySpawnYPos), enemyPrefab.transform.rotation);
            enemy.name = "enemy_wave_" + waveNumber + "_index_" + i;
        }
    }

    void SpawnRandomPowerUp()
    {
        int powerUpIndex = Random.Range(0, powerupPrefabs.Length);
        SpawnPowerup(powerUpIndex);
    }

    void SpawnPowerup(int powerUpIndex)
    {
        GameObject prefab = powerupPrefabs[powerUpIndex];
        Instantiate(prefab, GetRandomPositionOnIsland(powerupSpawnYPos), prefab.transform.rotation);
    }

    Vector3 GetRandomPositionOnIsland(float YPos)
    {
        float XPos = Random.Range(-spawnRange, spawnRange);
        float ZPos = Random.Range(-spawnRange, spawnRange);
        return new Vector3(XPos, YPos, ZPos);
    }
}
