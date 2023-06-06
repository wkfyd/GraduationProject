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

        //소크라테스
        if (get_Enemy_Id == 1001)
        {
            maxHealth = 2000000;
            AtkTurn = 6;
            atk_Damage = 10;
            SpTurn = 20;
            enemyName = "소크라테스";
        }

        //플라톤, 아리스토, 피타고라스
        else if (get_Enemy_Id == 1002 || get_Enemy_Id == 1003 || get_Enemy_Id == 1004)
        {
            maxHealth = 2000000;
            AtkTurn = 6;
            atk_Damage = 10;
            SpTurn = 17;

            if (get_Enemy_Id == 1002)
                enemyName = "플라톤";
            else if (get_Enemy_Id == 1003)
                enemyName = "아리스토텔레스";
            else if (get_Enemy_Id == 1004)
                enemyName = "피타고라스";
        }

        //아르키메, 탈레스, 에피쿠로스, 제논
        else if (get_Enemy_Id == 1005 || get_Enemy_Id == 1006 || get_Enemy_Id == 1007 || get_Enemy_Id == 1008)
        {
            maxHealth = 500000;
            AtkTurn = 4;
            atk_Damage = 8;
            SpTurn = 13;

            if (get_Enemy_Id == 1005)
                enemyName = "아르키메데스";
            else if (get_Enemy_Id == 1006)
                enemyName = "탈레스";
            else if (get_Enemy_Id == 1007)
                enemyName = "에피쿠로스";
            else if (get_Enemy_Id == 1008)
                enemyName = "키티움의 제논";
        }
        
        //디오게네스, 프로타, 트라시마, 고르기아스
        else if (get_Enemy_Id == 1009 || get_Enemy_Id == 1010 || get_Enemy_Id == 1011 || get_Enemy_Id == 1012)
        {
            maxHealth = 10000;
            AtkTurn = 5;
            atk_Damage = 10;
            SpTurn = 0;

            if (get_Enemy_Id == 1009)
                enemyName = "디오게네스";
            else if (get_Enemy_Id == 1010)
                enemyName = "프로타고라스";
            else if (get_Enemy_Id == 1011)
                enemyName = "트라시마코스";
            else if (get_Enemy_Id == 1012)
                enemyName = "고르기아스";
        }

        //히파소스, 유클리드, 스토아 학파, 에피쿠로스 학파, 소피스트
        else if (get_Enemy_Id == 1013 || get_Enemy_Id == 1014 ||
            get_Enemy_Id == 1015 || get_Enemy_Id == 1016 || get_Enemy_Id == 1017)
        {
            maxHealth = 500;
            AtkTurn = 4;
            atk_Damage = 7;
            SpTurn = 0;

            if (get_Enemy_Id == 1013)
                enemyName = "히파소스";
            else if (get_Enemy_Id == 1014)
                enemyName = "유클리드";
            else if (get_Enemy_Id == 1015)
                enemyName = "스토아 학파 철학자";
            else if (get_Enemy_Id == 1016)
                enemyName = "에피쿠로스 학파 철학자";
            else if (get_Enemy_Id == 1017)
                enemyName = "소피스트";
        }
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "나는 내가 아무것도 \n모른다는 것을 안다", "유일한 선은 앎이고 \n유일한 악은 무지다..." });
        talkData.Add(1002, new string[] { "친구는 모든 것을 나눈다.", "아아...나의 영혼은 \n이제 이데아계로..." });
        talkData.Add(1003, new string[] { "인간은 본성적으로 \n사회적 동물이다", "지나침이나 모자람엔 \n중용이 없고 중용엔 \n지나침이나 모자람이 없다..." });
        talkData.Add(1004, new string[] { "a²+b²=c²", "수의 조화가...\n깨져버리다니!" });
        talkData.Add(1005, new string[] { "유레카!", "내 원을 방해하지 말라!" });
        talkData.Add(1006, new string[] { "만물의 근원은 물로 \n이루어져 있다.", "세상은 신들로 \n가득 차 있다..." });
        talkData.Add(1007, new string[] { "빵과 물만 있다면 \n신도 부럽지 않다", "자네는 공허한 욕망에 \n사로잡혀 있군..." });
        talkData.Add(1008, new string[] { "자연과 일치되어 \n사는 것이 삶의 목적이다.", "내가 간다, 내가 간다. \n어찌하여 나를 부르느냐?" });
        talkData.Add(1009, new string[] { "따뜻한 햇빛을 \n가리고 계시는군요? \n비켜주시죠?", "나는 그냥...\n아무것도 필요 없어..." });
        talkData.Add(1010, new string[] { "인간은 만물의 척도다.", "문제는 너무나 \n불명확한 반면 \n인생은 짧다." });
        talkData.Add(1011, new string[] { "충분히 큰 규모의 불의는 \n정의보다 더 강하고, \n자유롭고, 지배적이오.", "정의는 강자의 이익일 뿐이다!" });
        talkData.Add(1012, new string[] { "수시학은 법원이나\n의회에서 의로운 것과\n불의한 것에 대해 설득하는\n기술입니다.", "아무것도 존재하지 않는다...\n존재하더라도 알 수 없다. \n알 수 있어도 전달 할 수 없다..." });
        talkData.Add(1013, new string[] { "아니~!!!\n이렇게 계산하면\n√2 나온다니까용!?", "이런 무리수를!" });
        talkData.Add(1014, new string[] { "기하학엔 왕도가\n없습니다.", "기하학!" });
        talkData.Add(1015, new string[] { "로고스 제일!", "충동을 따르는 삶을 \n살면...안 돼..." });
        talkData.Add(1016, new string[] { "아타락시아를,,,\n추구하는,,삶,,^^*", "무엇이 진정한 쾌락을 \n가져다 주는가...!" });
        talkData.Add(1017, new string[] { "누구지? 자네도 나의\n 가르침을 얻으러 왔나?", "고소하겠어!"});

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


    //id에 맞춰서 적 이미지 생성
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
