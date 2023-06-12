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
        switch (SaveData.currentStage)//)
        {
            case 1:
                pc_MaxHealth = 100;
                pc_Health = pc_MaxHealth;
                break;

            case 2:
                pc_MaxHealth = 130;
                pc_Health = pc_MaxHealth;
                break;

            case 3:
                pc_MaxHealth = 170;
                pc_Health = pc_MaxHealth;
                break;

            case 4:
                pc_MaxHealth = 290;
                pc_Health = pc_MaxHealth;
                break;

            case 5:
                pc_MaxHealth = 350;
                pc_Health = pc_MaxHealth;
                break;

            case 6:
                pc_MaxHealth = 450;
                pc_Health = pc_MaxHealth;
                break;
        }

    }

    void Update()
    {
        pc_HealthBar.fillAmount = (float)pc_Health / pc_MaxHealth;
    }
}
