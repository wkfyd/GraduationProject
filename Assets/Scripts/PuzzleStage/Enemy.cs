using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite[]> enemyImgData;

    public int maxHealth;
    public int health;
    public int AtkTurn;
    public int atk_Damage;
    public int SpTurn;

    public Sprite[] enemySprite;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        enemyImgData = new Dictionary<int, Sprite[]>();
        GenerateData();
    }
    void Start()
    {
        maxHealth = 500;
        health = maxHealth;
        AtkTurn = 4;
        atk_Damage = 7;
        SpTurn = 0;
    }

    void GenerateData()
    {
        talkData.Add(1017, new string[] { "누구지? 자네도 내 가르침을 얻으러 왔나?", "고소하겠어!"});

        enemyImgData.Add(1017, new Sprite[] { enemySprite[0], enemySprite[1], enemySprite[2] });
    }

    public Sprite[] GetSprite(int id)
    {
        return enemyImgData[id];
    }

    public string GetEnemyTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int Get_AtkTurn()
    {
        return AtkTurn;
    }

    public int Get_Atk_Damage()
    {
        return atk_Damage;
    }
    public int Get_SpTurn()
    {
        return SpTurn;
    }
}
