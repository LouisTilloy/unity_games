using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierData : MonoBehaviour
{
    // Side
    public bool friendly;

    // Stats
    public int attack;
    public int armor;
    public int maxHealth;

    // Current status
    public int currentHealth;

    SoldierData() { }

    SoldierData(bool friendly0, int attack0, int armor0, int maxHealth0, int currentHealth0) 
    { 
        friendly = friendly0;
        attack = attack0;
        armor = armor0;
        maxHealth = maxHealth0;
        currentHealth = currentHealth0;
    }

    public SoldierData Clone()
    {
        return new SoldierData(friendly, attack, armor, maxHealth, currentHealth);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }


}
