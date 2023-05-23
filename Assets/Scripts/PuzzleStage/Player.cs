using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image healthBar;

    public int pc_MaxHealth;
    public int pc_Health;

    void Start()
    {
        pc_MaxHealth = 100;
        pc_Health = pc_MaxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = (float)pc_Health / pc_MaxHealth;
    }
}
