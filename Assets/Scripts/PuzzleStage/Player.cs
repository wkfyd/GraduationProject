using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image pc_HealthBar;

    public int pc_MaxHealth;
    public int pc_Health;

    void Start()
    {
        pc_MaxHealth = 100;
        pc_Health = pc_MaxHealth;
    }

    void Update()
    {
        pc_HealthBar.fillAmount = (float)pc_Health / pc_MaxHealth;
    }
}
