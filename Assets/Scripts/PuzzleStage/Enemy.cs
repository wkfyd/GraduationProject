using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int next_Atk;
    public int next_Sp;
    public int atk_Damage;

    void Start()
    {
        maxHealth = 500;
        health = maxHealth;
        next_Atk = 4;
        atk_Damage = 7;
        next_Sp = 0;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int Get_Next_Atk()
    {
        return next_Atk;
    }

    public int Get_Atk_Damage()
    {
        return atk_Damage;
    }
}
