using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthGauge : MonoBehaviour
{
    Image healthBar;
    float maxHP = 100f;
    public static float health;

    void Awake()
    {
        healthBar = GetComponent<Image>();
    }
    void Start()
    {
        healthBar = GetComponent<Image>();
        health = maxHP;
    }

    void Update()
    {
        healthBar.fillAmount = health / maxHP;
    }

    public void adnfgln()
    {
        health -= 10;
    }
}
