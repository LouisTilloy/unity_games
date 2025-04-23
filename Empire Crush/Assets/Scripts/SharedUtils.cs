using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum AttackStatus
{
    Win,
    Loss,
    Draw
}

public class SharedUtils
{
    const int MAX_FIGHT_ITERATIONS = 1000;

    static public AttackStatus CityFightSimple(int nSoldiersAttack, int nSoldiersDefense)
    {
        if (nSoldiersAttack > nSoldiersDefense)
        {
            return AttackStatus.Win;
        }
        else if (nSoldiersAttack < nSoldiersDefense)
        {
            return AttackStatus.Loss;
        }
        else
        {
            return AttackStatus.Draw;
        }
    }

    static public List<int> CityFight(int nSoldiersAttack, int nSoldiersDefense, int baseArmor, int baseHp, List<int> baseDamageRange, int defBonusArmor, int defBonusDamage)
    {
        if (baseDamageRange.Count != 2)
        {
            throw new ArgumentException("Damage range should be of size 2.");
        }

        if (nSoldiersAttack == 0 || nSoldiersDefense == 0)
        {
            return new List<int> { nSoldiersAttack, nSoldiersDefense };
        }

        List<int> attackers = Enumerable.Repeat(baseHp, nSoldiersAttack).ToList();
        List<int> defenders = Enumerable.Repeat(baseHp, nSoldiersDefense).ToList();
        int targetDefender;
        int targetAttacker;
        int damage;
        int finalDamage;
        for (int i = 0; i < MAX_FIGHT_ITERATIONS; i++)
        {
            // Attacker initiative, defender can have bonus armor
            foreach (int attacker in attackers)
            {
                targetDefender = UnityEngine.Random.Range(0, defenders.Count);
                damage = UnityEngine.Random.Range(baseDamageRange[0], baseDamageRange[1]);
                finalDamage = Mathf.Max(1, damage - baseArmor - defBonusArmor);
                defenders[targetDefender] -= finalDamage;
                if (defenders[targetDefender] <= 0)
                {
                    defenders.RemoveAt(targetDefender);
                    if (defenders.Count == 0)
                    {
                        return new List<int> { attackers.Count, defenders.Count };
                    }
                }
            }
            // Defender response, defender can have bonus damage
            foreach (int defender in defenders)
            {
                targetAttacker = UnityEngine.Random.Range(0, attackers.Count);
                damage = UnityEngine.Random.Range(baseDamageRange[0], baseDamageRange[1]);
                finalDamage = Mathf.Max(1, damage + defBonusDamage - baseArmor);
                attackers[targetAttacker] -= finalDamage;
                if (attackers[targetAttacker] <= 0)
                {
                    attackers.RemoveAt(targetAttacker);
                    if (attackers.Count == 0)
                    {
                        return new List<int> { attackers.Count, defenders.Count };
                    }
                }
            }
        }
        throw new TimeoutException("Fight system stuck.");
    }
}
