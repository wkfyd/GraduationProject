using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public Enemy enemy;

    public Image healthBar;

    public int enemy_maxHealth;
    public int enemy_health;
    public int enemy_next_Atk;
    public int enemy_next_Sp;
    public int enemy_atk_Damage;

    void Start()
    {
        enemy_maxHealth = enemy.GetMaxHealth();
        enemy_health = enemy.GetHealth();
        enemy_next_Atk = enemy.Get_Next_Atk();
        enemy_atk_Damage = enemy.Get_Atk_Damage();
    }

    void Update()
    {
        healthBar.fillAmount = (float)enemy_health / enemy_maxHealth;

    }
}
