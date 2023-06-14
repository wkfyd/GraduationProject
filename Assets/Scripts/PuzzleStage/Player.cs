using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image pc_HealthBar;

    public int pc_MaxHealth;
    public int pc_Health;
    public int pc_curntHealth;

    void Start()
    {
        switch (SaveData.currentStage)//)
        {
            case 1:
                pc_MaxHealth = 100;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;

            case 2:
                pc_MaxHealth = 130;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;

            case 3:
                pc_MaxHealth = 170;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;

            case 4:
                pc_MaxHealth = 290;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;

            case 5:
                pc_MaxHealth = 350;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;

            case 6:
                pc_MaxHealth = 450;
                pc_Health = pc_MaxHealth;
                pc_curntHealth = pc_Health;
                break;
        }

    }

    void Update()
    {
        pc_HealthBar.fillAmount = (float)pc_Health / pc_MaxHealth;
    }
}
