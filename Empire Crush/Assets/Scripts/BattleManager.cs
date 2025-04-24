using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using Army = System.Collections.Generic.List<SoldierData>;
using FightLogs = System.Collections.Generic.List<string>;
// Format: 2a->3b:5 => Soldier from Army a with ID 2 attacks Soldier from Army b with ID 3 for a total of 5 damage (final).
// Example:
// 2a->3b:5
// 3a->2b:2
// ...

public class BattleManager : MonoBehaviour
{
    const int MAX_FIGHT_ITERATIONS = 1000;
    [SerializeField] float timeBeforeStart = 4;
    [SerializeField] float fightSpeed;
    [SerializeField] bool launchFight = false;

    private void OnEnable()
    {
        StartCoroutine(WaitAndLaunchFight());
    }

    IEnumerator WaitAndLaunchFight()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        launchFight = true;
    }

    private void Update()
    {
        // Manual debug button to launch fight in editor.
        if (launchFight)
        {
            launchFight = false;
            SoldierData[] allSoldiers = transform.parent.GetComponentsInChildren<SoldierData>();
            Army friendlyArmy = new();
            Army enemyArmy = new();
            foreach (var soldier in allSoldiers)
            {
                if (soldier.friendly)
                {
                    friendlyArmy.Add(soldier);
                }
                else
                {
                    enemyArmy.Add(soldier);
                }
            }
            StartCoroutine(FightArmies(friendlyArmy, enemyArmy));
        }
    }

    bool AllSoldiersAreDead(Army army)
    {
        return IndexesSoldiersAlive(army).Count == 0;
    }

    List<int> IndexesSoldiersAlive(Army army)
    {
        List<int> indexes = new();
        SoldierData soldier;
        for (int i = 0; i < army.Count; i++)
        {
            soldier = army[i];
            if (!soldier.IsDead())
            {
                indexes.Add(i);
            }
        }
        return indexes;
    }

    void RemoveIfDead(Army army, int idx)
    {
        if (army[idx].IsDead())
        {
            Destroy(army[idx].gameObject, 0.5f);
            army.RemoveAt(idx);
        }
    }

    IEnumerator FightArmies(Army army1, Army army2, bool army1AttacksFirst = true)
    {
        // Assign order of attacks.
        List<Army> armies;
        if (army1AttacksFirst)
        {
            armies = new() { army1, army2 };
        }
        else
        {
            armies = new() { army2, army1 };
        }

        // Initialize variables.
        FightLogs fightLogs = new();
        int targetIdx;
        Army attackers;
        Army defenders;
        SoldierData activeSoldier;
        SoldierData targetSoldier;
        int damage;
        int finalDamage;

        // Fight for a maximum of turns.
        for (int i = 0; i < MAX_FIGHT_ITERATIONS; i++)
        {
            // Each army takes its turn.
            for (int armyIdx = 0; armyIdx < 2; armyIdx++)
            {
                attackers = armies[armyIdx];
                defenders = armies[1 - armyIdx];

                // Each soldier alive takes its attack.
                for (int attackerIdx = 0; attackerIdx < attackers.Count; attackerIdx++)
                {
                    activeSoldier = attackers[attackerIdx];

                    // Choose target.
                    targetIdx = Random.Range(0, defenders.Count);
                    targetSoldier = defenders[targetIdx];

                    // Compute damage.
                    damage = Random.Range(activeSoldier.attack - 1, activeSoldier.attack + 2);
                    finalDamage = Mathf.Max(0, damage - targetSoldier.armor);

                    // Apply damage to soldier.
                    targetSoldier.currentHealth -= finalDamage;
                    RemoveIfDead(defenders, targetIdx);

                    // Update logs.
                    fightLogs.Add($"{armyIdx}:{attackerIdx}->{targetIdx}:{finalDamage}");
                    Debug.Log($"{armyIdx}:{attackerIdx}->{targetIdx}:{finalDamage}");

                    // Wait for the player to follow the battle.
                    yield return new WaitForSeconds( 1.0f / fightSpeed);
                    
                    // Stop the fight if one side is dead.
                    if (AllSoldiersAreDead(defenders))
                    {
                        yield break;
                    }
                }
            }
        }
    }

    // BOOK-KEEPING: NOT USEFUL FOR NOW BUT COULD BE USEFUL LATER ON
    // Returns: The friendly army in its final stage, the enemy Army in its final stage and the log of the fight.
    (Army, Army, FightLogs) FightAndGetFightingLog(Army army1, Army army2, bool army1AttacksFirst = true)
    {
        // Assign order of attacks
        List<Army> armies;
        if (army1AttacksFirst)
        {
            armies = new() { army1, army2 };
        }
        else
        {
            armies = new() { army2, army1 };
        }
        
        // Initialize variables
        FightLogs fightLogs = new();
        int targetIdx;
        Army attackers;
        Army defenders;
        SoldierData activeSoldier;
        SoldierData targetSoldier;
        int damage;
        int finalDamage;

        // Fight for a maximum of turns
        for (int i = 0; i < MAX_FIGHT_ITERATIONS; i++)
        {
            // Each army takes its turn
            for (int armyIdx = 0; armyIdx < 2; armyIdx++)
            {
                attackers = armies[armyIdx];
                defenders = armies[1 - armyIdx];
                // Each soldier takes its attack
                for (int attackerIdx = 0; attackerIdx < attackers.Count; attackerIdx++)
                {
                    activeSoldier = attackers[attackerIdx];
                    targetIdx = Random.Range(0, defenders.Count);
                    targetSoldier = defenders[targetIdx];
                    damage = Random.Range(activeSoldier.attack - 1, activeSoldier.attack + 1);
                    finalDamage = Mathf.Max(1, damage - targetSoldier.armor);
                    targetSoldier.currentHealth -= finalDamage;
                    fightLogs.Add($"{attackerIdx}a->{targetIdx}b:{finalDamage}");
                    
                    if (AllSoldiersAreDead(defenders))
                    {
                        return (army1, army2, fightLogs);
                    }
                }
            }
        }
        return (army1, army2, fightLogs);
    }
}
