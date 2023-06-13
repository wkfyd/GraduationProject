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
    public string spNameText;
    public string spText;

    public Sprite[] enemySprite;
    public Sprite[] enemy_Sp_Atk;

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
        SetEnemySp();

        switch (get_Enemy_Id)
        {
            //��ũ���׽�
            case 1001:
                maxHealth = 1200000;
                AtkTurn = 6;
                atk_Damage = 20;
                SpTurn = 31;
                enemyName = "��ũ���׽�";
                spNameText = "������ ���ļ�";
                spText = "��ũ���׽��� ��ī�ο� ������ ����\n����� HP�� �������� �����.";
                break;

            //�ö���
            case 1002:
                maxHealth = 1200000;
                AtkTurn = 8;
                atk_Damage = 21;
                SpTurn = 25;
                enemyName = "�ö���";
                spNameText = "�̵��� �ǽ�Ʈ";
                spText = "�ö����� ������ ö�� �ָ�����\n������ ������� ������.";
                break;

            //�Ƹ�����
            case 1003:

                maxHealth = 1200000;
                AtkTurn = 5;
                atk_Damage = 25;
                SpTurn = 17;
                enemyName = "�Ƹ������ڷ���";
                spNameText = "���� ���� ����ħ";
                spText = "�Ƹ������ڷ����� 5���� ����\n�ڽ��� �޴� ������� 0���� �����.";
                break;

            //��Ÿ���
            case 1004:
                maxHealth = 1200000;
                AtkTurn = 6;
                atk_Damage = 15;
                SpTurn = 25;
                enemyName = "��Ÿ���";
                spNameText = "KOSMOS";
                spText = "��Ÿ��󽺰� ��ȭ�� ������� ����\n�Ϻ��� ������� ������.";
                break;

            //�Ƹ�Ű�޵���
            case 1005:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 8;
                SpTurn = 25;
                enemyName = "�Ƹ�Ű�޵���";
                spNameText = "���� ����";
                spText = "<size=30>�Ƹ�Ű�޵����� �� ����ϸ�\n�ٰ��� �������� ����� �߻��Ѵ�.\n7�� ���� ���� ���� ������\n8 ������� ������.</size>";
                break;

            //Ż����
            case 1006:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 8;
                SpTurn = 25;
                enemyName = "Ż����";
                spNameText = "�Ƹ���: ��";
                spText = "������ �ٿ��� ���� ����� �ֺ��� ���Ѵ�.\n2���� ���� ����� �޴� �������\n4�谡 �ȴ�.";
                break;

            //������ν�
            case 1007:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 15;
                SpTurn = 25;
                enemyName = "������ν�";
                spNameText = "��Ÿ���þ�";
                spText = "<size=30>������ν��� �ڽ��� �ı��� ����\n����� Ž���� �����Ѵ�.\n3���� ���� ����� ���ϴ� ����� ��ġ�� \n{ 2 / 3 }�� �ȴ�.</size>";
                break;

            //ŰƼ���� ����
            case 1008:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 10;
                SpTurn = 25;
                enemyName = "ŰƼ���� ����";
                spNameText = "������ �ΰ�";
                spText = "������ ��Ȱ�� ���ֿ��� ���� ��\n0.2�ʸ��� 7 ������� ������.\n�� 5ȸ �����Ѵ�.";
                break;

            //����Գ׽�
            case 1009:
                maxHealth = 3000;
                AtkTurn = 8;
                atk_Damage = 10;
                SpTurn = 0;
                enemyName = "����Գ׽�";
                break;

            //����Ÿ, ����ƽ�
            case 1010:
            case 1012:
                maxHealth = 2500;
                AtkTurn = 5;
                atk_Damage = 10;
                SpTurn = 0;

                if (get_Enemy_Id == 1010)
                    enemyName = "����Ÿ���";
                else if (get_Enemy_Id == 1011)
                    enemyName = "Ʈ��ø��ڽ�";
                else if (get_Enemy_Id == 1012)
                    enemyName = "����ƽ�";
                break;

            //Ʈ��ø�
            case 1011:
                maxHealth = 2400;
                AtkTurn = 7;
                atk_Damage = 15;
                SpTurn = 0;
                enemyName = "Ʈ��ø��ڽ�";
                break;

            //���ļҽ�, ��Ŭ����, ����� ����, ������ν� ����
            case 1013:
            case 1014:
            case 1015:
            case 1016:
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
                break;

            //���ǽ�Ʈ
            case 1017:
                maxHealth = 450;
                AtkTurn = 3;
                atk_Damage = 5;
                SpTurn = 0;
                enemyName = "���ǽ�Ʈ";
                break;

        }
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "���� ���� �ƹ��͵�\n�𸥴ٴ� ���� �ȴ�.", "������ ���� ���̰�\n������ ���� ������..." });
        talkData.Add(1002, new string[] { "ģ����\n��� ���� ������.", "�ƾ�...���� ��ȥ��\n���� �̵��ư��..." });
        talkData.Add(1003, new string[] { "�ΰ��� ����������\n��ȸ�� �����̴�.", "����ħ�̳� ���ڶ���\n�߿��� ���� �߿뿣\n����ħ�̳� ���ڶ���\n����..." });
        talkData.Add(1004, new string[] { "a��+b��=c��", "���� ��ȭ��...\n���������ٴ�!" });
        talkData.Add(1005, new string[] { "����ī!", "�� ���� �������� ����!" });
        talkData.Add(1006, new string[] { "������ �ٿ���\n���� �̷���� �ִ�.", "������ �ŵ��\n���� �� �ִ�..." });
        talkData.Add(1007, new string[] { "���� ���� �ִٸ�\n�ŵ� �η��� �ʴ�.", "�ڳ״� ������ �����\n������� �ֱ�..." });
        talkData.Add(1008, new string[] { "�ڿ���\n��ġ�Ǿ� ��� ����\n���� �����̴�.", "���� ����, ���� ����.\n�����Ͽ� ���� �θ�����?" });
        talkData.Add(1009, new string[] { "������ �޺���\n������ ��ô±���?\n�����ֽ���?", "���� �׳�...\n�ƹ��͵� �ʿ� ����..." });
        talkData.Add(1010, new string[] { "�ΰ��� ������ ô����.", "������ �ʹ��� \n�Ҹ�Ȯ�� �ݸ� \n�λ��� ª��." });
        talkData.Add(1011, new string[] { "����� ū �Ը��� ���Ǵ� \n���Ǻ��� �� ���ϰ�,\n�����Ӱ�, �������̿�.", "���Ǵ� ������\n������ ���̴�!" });
        talkData.Add(1012, new string[] { "�������� �����̳�\n��ȸ���� �Ƿο� �Ͱ�\n������ �Ϳ� ����\n�����ϴ� ����Դϴ�.", "�ƹ��͵� �������� �ʴ´�...\n�����ϴ��� �� �� ����.\n�� �� �־ ������ ��\n����..." });
        talkData.Add(1013, new string[] { "�ƴ�~!!!\n�̷��� ����ϸ�\n��2 ���´ٴϱ��!?", "�̷� ��������!" });
        talkData.Add(1014, new string[] { "�����п�\n�յ��� �����ϴ�.", "������!" });
        talkData.Add(1015, new string[] { "�ΰ� ����!", "�浿�� ������ ���� \n���...�� ��..." });
        talkData.Add(1016, new string[] { "��Ÿ���þƸ�,,,\n�߱��ϴ�,,��,,^^*", "������ ������ ����� \n������ �ִ°�...!" });
        talkData.Add(1017, new string[] { "������? �ڳ׵� ����\n����ħ�� ������ �Գ�?", "����ϰھ�!" });

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


    //id�� ���缭 �� �̹��� Ȱ��ȭ
    void EnemySetAct()
    {
        GameObject[] findEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < 17; i++)
        {
            if (get_Enemy_Id != findEnemy[i].GetComponent<NPCData>().id)
                findEnemy[i].SetActive(false);
        }
    }

    //id�� ���� �� ����� ���� ����
    public Sprite SetEnemySp()
    {
        Sprite spImg = null;

        switch (get_Enemy_Id)
        {
            case 1001:
                spImg = enemy_Sp_Atk[0];
                break;

            case 1002:
                spImg = enemy_Sp_Atk[1];
                break;

            case 1003:
                spImg = enemy_Sp_Atk[2];
                break;

            case 1004:
                spImg = enemy_Sp_Atk[3];
                break;

            case 1005:
                spImg = enemy_Sp_Atk[4];
                break;

            case 1006:
                spImg = enemy_Sp_Atk[5];
                break;

            case 1007:
                spImg = enemy_Sp_Atk[6];
                break;

            case 1008:
                spImg = enemy_Sp_Atk[7];
                break;
        }

        return spImg;
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

    public string Get_SpNameText()
    {
        return spNameText;
    }

    public string Get_SpText()
    {
        return spText;
    }
}
