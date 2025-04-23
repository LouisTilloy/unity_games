using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFightManager : MonoBehaviour
{
    [SerializeField] CityData attackerCityData;

    // General fight parameters
    const int baseArmor = 0;
    const int baseHp = 10;
    List<int> baseDamageRange = new() { 2, 5};
    const int defBonusArmor = 1;
    const int defBonusDamage = 0;



    public void CityFight(CityData defenderCityData)
    {
        List<int> nAttackersDefenders = SharedUtils.CityFight(attackerCityData.nSoldiers, defenderCityData.nSoldiers, baseArmor, baseHp, baseDamageRange, defBonusArmor, defBonusDamage);
        ResolveAttack(nAttackersDefenders[0], nAttackersDefenders[1], defenderCityData);
    }

    // Resolve the attack based on the amount of soldiers left.
    void ResolveAttack(int nAttackers, int nDefenders, CityData defenderCityData)
    {
        attackerCityData.nSoldiers = nAttackers;
        defenderCityData.nSoldiers = nDefenders;
    }

    /*
    void ResolveAttack(int nAttackers, int nDefenders, CityData defenderCityData)
    {
        switch (attackStatus)
        {
            case AttackStatus.Win:
                Debug.Log("Attacker wins!");
                defenderCityData.nSoldiers = 1;
                break;
            case AttackStatus.Loss:
                Debug.Log("Attacker loses!");
                attackerCityData.nSoldiers = 0;
                break;
            case AttackStatus.Draw:
                Debug.Log("Draw!");
                defenderCityData.nSoldiers -= 1;
                attackerCityData.nSoldiers -= 1;
                break;
            default:
                break;
        }
    }
    */
}
