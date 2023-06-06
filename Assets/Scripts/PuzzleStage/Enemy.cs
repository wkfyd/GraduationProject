using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite[]> enemyImgData;

    public int get_Enemy_Id;
    public int maxHealth;
    public int AtkTurn;
    public int atk_Damage;
    public int SpTurn;
    public string enemyName;

    public Sprite[] enemySprite;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        enemyImgData = new Dictionary<int, Sprite[]>();
        GenerateData();
    }
    void Start()
    {
        get_Enemy_Id = SaveData.currentEnemy_Id;
        EnemySetAct();

        //��ũ���׽�
        if (get_Enemy_Id == 1001)
        {
            maxHealth = 2000000;
            AtkTurn = 6;
            atk_Damage = 10;
            SpTurn = 20;
            enemyName = "��ũ���׽�";
        }

        //�ö���, �Ƹ�����, ��Ÿ���
        else if (get_Enemy_Id == 1002 || get_Enemy_Id == 1003 || get_Enemy_Id == 1004)
        {
            maxHealth = 2000000;
            AtkTurn = 6;
            atk_Damage = 10;
            SpTurn = 17;

            if (get_Enemy_Id == 1002)
                enemyName = "�ö���";
            else if (get_Enemy_Id == 1003)
                enemyName = "�Ƹ������ڷ���";
            else if (get_Enemy_Id == 1004)
                enemyName = "��Ÿ���";
        }

        //�Ƹ�Ű��, Ż����, ������ν�, ����
        else if (get_Enemy_Id == 1005 || get_Enemy_Id == 1006 || get_Enemy_Id == 1007 || get_Enemy_Id == 1008)
        {
            maxHealth = 500000;
            AtkTurn = 4;
            atk_Damage = 8;
            SpTurn = 13;

            if (get_Enemy_Id == 1005)
                enemyName = "�Ƹ�Ű�޵���";
            else if (get_Enemy_Id == 1006)
                enemyName = "Ż����";
            else if (get_Enemy_Id == 1007)
                enemyName = "������ν�";
            else if (get_Enemy_Id == 1008)
                enemyName = "ŰƼ���� ����";
        }
        
        //����Գ׽�, ����Ÿ, Ʈ��ø�, ����ƽ�
        else if (get_Enemy_Id == 1009 || get_Enemy_Id == 1010 || get_Enemy_Id == 1011 || get_Enemy_Id == 1012)
        {
            maxHealth = 10000;
            AtkTurn = 5;
            atk_Damage = 10;
            SpTurn = 0;

            if (get_Enemy_Id == 1009)
                enemyName = "����Գ׽�";
            else if (get_Enemy_Id == 1010)
                enemyName = "����Ÿ���";
            else if (get_Enemy_Id == 1011)
                enemyName = "Ʈ��ø��ڽ�";
            else if (get_Enemy_Id == 1012)
                enemyName = "����ƽ�";
        }

        //���ļҽ�, ��Ŭ����, ����� ����, ������ν� ����, ���ǽ�Ʈ
        else if (get_Enemy_Id == 1013 || get_Enemy_Id == 1014 ||
            get_Enemy_Id == 1015 || get_Enemy_Id == 1016 || get_Enemy_Id == 1017)
        {
            maxHealth = 500;
            AtkTurn = 4;
            atk_Damage = 7;
            SpTurn = 0;

            if (get_Enemy_Id == 1013)
                enemyName = "���ļҽ�";
            else if (get_Enemy_Id == 1014)
                enemyName = "��Ŭ����";
            else if (get_Enemy_Id == 1015)
                enemyName = "����� ���� ö����";
            else if (get_Enemy_Id == 1016)
                enemyName = "������ν� ���� ö����";
            else if (get_Enemy_Id == 1017)
                enemyName = "���ǽ�Ʈ";
        }
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "���� ���� �ƹ��͵� \n�𸥴ٴ� ���� �ȴ�", "������ ���� ���̰� \n������ ���� ������..." });
        talkData.Add(1002, new string[] { "ģ���� ��� ���� ������.", "�ƾ�...���� ��ȥ�� \n���� �̵��ư��..." });
        talkData.Add(1003, new string[] { "�ΰ��� ���������� \n��ȸ�� �����̴�", "����ħ�̳� ���ڶ��� \n�߿��� ���� �߿뿣 \n����ħ�̳� ���ڶ��� ����..." });
        talkData.Add(1004, new string[] { "a��+b��=c��", "���� ��ȭ��...\n���������ٴ�!" });
        talkData.Add(1005, new string[] { "����ī!", "�� ���� �������� ����!" });
        talkData.Add(1006, new string[] { "������ �ٿ��� ���� \n�̷���� �ִ�.", "������ �ŵ�� \n���� �� �ִ�..." });
        talkData.Add(1007, new string[] { "���� ���� �ִٸ� \n�ŵ� �η��� �ʴ�", "�ڳ״� ������ ����� \n������� �ֱ�..." });
        talkData.Add(1008, new string[] { "�ڿ��� ��ġ�Ǿ� \n��� ���� ���� �����̴�.", "���� ����, ���� ����. \n�����Ͽ� ���� �θ�����?" });
        talkData.Add(1009, new string[] { "������ �޺��� \n������ ��ô±���? \n�����ֽ���?", "���� �׳�...\n�ƹ��͵� �ʿ� ����..." });
        talkData.Add(1010, new string[] { "�ΰ��� ������ ô����.", "������ �ʹ��� \n�Ҹ�Ȯ�� �ݸ� \n�λ��� ª��." });
        talkData.Add(1011, new string[] { "����� ū �Ը��� ���Ǵ� \n���Ǻ��� �� ���ϰ�, \n�����Ӱ�, �������̿�.", "���Ǵ� ������ ������ ���̴�!" });
        talkData.Add(1012, new string[] { "�������� �����̳�\n��ȸ���� �Ƿο� �Ͱ�\n������ �Ϳ� ���� �����ϴ�\n����Դϴ�.", "�ƹ��͵� �������� �ʴ´�...\n�����ϴ��� �� �� ����. \n�� �� �־ ���� �� �� ����..." });
        talkData.Add(1013, new string[] { "�ƴ�~!!!\n�̷��� ����ϸ�\n��2 ���´ٴϱ��!?", "�̷� ��������!" });
        talkData.Add(1014, new string[] { "�����п� �յ���\n�����ϴ�.", "������!" });
        talkData.Add(1015, new string[] { "�ΰ� ����!", "�浿�� ������ ���� \n���...�� ��..." });
        talkData.Add(1016, new string[] { "��Ÿ���þƸ�,,,\n�߱��ϴ�,,��,,^^*", "������ ������ ����� \n������ �ִ°�...!" });
        talkData.Add(1017, new string[] { "������? �ڳ׵� ����\n ����ħ�� ������ �Գ�?", "����ϰھ�!"});

        enemyImgData.Add(1001, new Sprite[] { enemySprite[0], enemySprite[1], enemySprite[2] });
        enemyImgData.Add(1002, new Sprite[] { enemySprite[3], enemySprite[4], enemySprite[5] });
        enemyImgData.Add(1003, new Sprite[] { enemySprite[6], enemySprite[7], enemySprite[8] });
        enemyImgData.Add(1004, new Sprite[] { enemySprite[9], enemySprite[10], enemySprite[11] });
        enemyImgData.Add(1005, new Sprite[] { enemySprite[12], enemySprite[13], enemySprite[14] });
        enemyImgData.Add(1006, new Sprite[] { enemySprite[15], enemySprite[16], enemySprite[17] });
        enemyImgData.Add(1007, new Sprite[] { enemySprite[18], enemySprite[19], enemySprite[20] });
        enemyImgData.Add(1008, new Sprite[] { enemySprite[21], enemySprite[22], enemySprite[23] });
        enemyImgData.Add(1009, new Sprite[] { enemySprite[24], enemySprite[25], enemySprite[26] });
        enemyImgData.Add(1010, new Sprite[] { enemySprite[27], enemySprite[28], enemySprite[29] });
        enemyImgData.Add(1011, new Sprite[] { enemySprite[30], enemySprite[31], enemySprite[32] });
        enemyImgData.Add(1012, new Sprite[] { enemySprite[33], enemySprite[34], enemySprite[35] });
        enemyImgData.Add(1013, new Sprite[] { enemySprite[36], enemySprite[37], enemySprite[38] });
        enemyImgData.Add(1014, new Sprite[] { enemySprite[39], enemySprite[40], enemySprite[41] });
        enemyImgData.Add(1015, new Sprite[] { enemySprite[42], enemySprite[43], enemySprite[44] });
        enemyImgData.Add(1016, new Sprite[] { enemySprite[45], enemySprite[46], enemySprite[47] });
        enemyImgData.Add(1017, new Sprite[] { enemySprite[48], enemySprite[49], enemySprite[50] });
    }


    //id�� ���缭 �� �̹��� ����
    void EnemySetAct()
    {
        GameObject[] findEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < 17; i++)
        {
            if (get_Enemy_Id != findEnemy[i].GetComponent<NPCData>().id)
                findEnemy[i].SetActive(false);
        }
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

    public string Get_enemyName()
    {
        return enemyName;
    }
}
