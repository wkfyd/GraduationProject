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
            //소크라테스
            case 1001:
                maxHealth = 1200000;
                AtkTurn = 6;
                atk_Damage = 20;
                SpTurn = 31;
                enemyName = "소크라테스";
                spNameText = "진리의 산파술";
                spText = "소크라테스가 날카로운 질문을 던져\n당신의 HP를 절반으로 만든다.";
                break;

            //플라톤
            case 1002:
                maxHealth = 1200000;
                AtkTurn = 8;
                atk_Damage = 21;
                SpTurn = 25;
                enemyName = "플라톤";
                spNameText = "이데아 피스트";
                spText = "플라톤이 강력한 철인 주먹으로\n강력한 대미지를 입힌다.";
                break;

            //아리스토
            case 1003:

                maxHealth = 1200000;
                AtkTurn = 5;
                atk_Damage = 25;
                SpTurn = 17;
                enemyName = "아리스토텔레스";
                spNameText = "덕에 대한 가르침";
                spText = "아리스토텔레스가 5번에 한해\n자신이 받는 대미지를 0으로 만든다.";
                break;

            //피타고라스
            case 1004:
                maxHealth = 1200000;
                AtkTurn = 6;
                atk_Damage = 15;
                SpTurn = 25;
                enemyName = "피타고라스";
                spNameText = "KOSMOS";
                spText = "피타고라스가 조화의 원기옥을 날려\n완벽한 대미지를 입힌다.";
                break;

            //아르키메데스
            case 1005:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 8;
                SpTurn = 25;
                enemyName = "아르키메데스";
                spNameText = "π의 역습";
                spText = "<size=30>아르키메데스가 π를 계산하며\n다각형 마법진을 만들어 발사한다.\n7턴 동안 턴이 끝날 때마다\n8 대미지를 입힌다.</size>";
                break;

            //탈레스
            case 1006:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 8;
                SpTurn = 25;
                enemyName = "탈레스";
                spNameText = "아르케: 물";
                spText = "만물의 근원인 물이 당신의 주변을 감싼다.\n2번에 한해 당신이 받는 대미지가\n4배가 된다.";
                break;

            //에피쿠로스
            case 1007:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 15;
                SpTurn = 25;
                enemyName = "에피쿠로스";
                spNameText = "아타락시아";
                spText = "<size=30>에피쿠로스가 자신의 후광을 비춰\n당신의 탐욕을 제거한다.\n3번에 한해 당신이 가하는 대미지 수치가 \n{ 2 / 3 }가 된다.</size>";
                break;

            //키티움의 제논
            case 1008:
                maxHealth = 600000;
                AtkTurn = 4;
                atk_Damage = 10;
                SpTurn = 25;
                enemyName = "키티움의 제논";
                spNameText = "위대한 로고스";
                spText = "제논이 광활한 우주에서 빔을 쏴\n0.2초마다 7 대미지를 입힌다.\n총 5회 공격한다.";
                break;

            //디오게네스
            case 1009:
                maxHealth = 3000;
                AtkTurn = 8;
                atk_Damage = 10;
                SpTurn = 0;
                enemyName = "디오게네스";
                break;

            //프로타, 고르기아스
            case 1010:
            case 1012:
                maxHealth = 2500;
                AtkTurn = 5;
                atk_Damage = 10;
                SpTurn = 0;

                if (get_Enemy_Id == 1010)
                    enemyName = "프로타고라스";
                else if (get_Enemy_Id == 1011)
                    enemyName = "트라시마코스";
                else if (get_Enemy_Id == 1012)
                    enemyName = "고르기아스";
                break;

            //트라시마
            case 1011:
                maxHealth = 2400;
                AtkTurn = 7;
                atk_Damage = 15;
                SpTurn = 0;
                enemyName = "트라시마코스";
                break;

            //히파소스, 유클리드, 스토아 학파, 에피쿠로스 학파
            case 1013:
            case 1014:
            case 1015:
            case 1016:
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
                break;

            //소피스트
            case 1017:
                maxHealth = 450;
                AtkTurn = 3;
                atk_Damage = 5;
                SpTurn = 0;
                enemyName = "소피스트";
                break;

        }
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "나는 내가 아무것도\n모른다는 것을 안다.", "유일한 선은 앎이고\n유일한 악은 무지다..." });
        talkData.Add(1002, new string[] { "친구는\n모든 것을 나눈다.", "아아...나의 영혼은\n이제 이데아계로..." });
        talkData.Add(1003, new string[] { "인간은 본성적으로\n사회적 동물이다.", "지나침이나 모자람엔\n중용이 없고 중용엔\n지나침이나 모자람이\n없다..." });
        talkData.Add(1004, new string[] { "a²+b²=c²", "수의 조화가...\n깨져버리다니!" });
        talkData.Add(1005, new string[] { "유레카!", "내 원을 방해하지 말라!" });
        talkData.Add(1006, new string[] { "만물의 근원은\n물로 이루어져 있다.", "세상은 신들로\n가득 차 있다..." });
        talkData.Add(1007, new string[] { "빵과 물만 있다면\n신도 부럽지 않다.", "자네는 공허한 욕망에\n사로잡혀 있군..." });
        talkData.Add(1008, new string[] { "자연과\n일치되어 사는 것이\n삶의 목적이다.", "내가 간다, 내가 간다.\n어찌하여 나를 부르느냐?" });
        talkData.Add(1009, new string[] { "따뜻한 햇빛을\n가리고 계시는군요?\n비켜주시죠?", "나는 그냥...\n아무것도 필요 없어..." });
        talkData.Add(1010, new string[] { "인간은 만물의 척도다.", "문제는 너무나 \n불명확한 반면 \n인생은 짧다." });
        talkData.Add(1011, new string[] { "충분히 큰 규모의 불의는 \n정의보다 더 강하고,\n자유롭고, 지배적이오.", "정의는 강자의\n이익일 뿐이다!" });
        talkData.Add(1012, new string[] { "수사학은 법원이나\n의회에서 의로운 것과\n불의한 것에 대해\n설득하는 기술입니다.", "아무것도 존재하지 않는다...\n존재하더라도 알 수 없다.\n알 수 있어도 전달할 수\n없다..." });
        talkData.Add(1013, new string[] { "아니~!!!\n이렇게 계산하면\n√2 나온다니까용!?", "이런 무리수를!" });
        talkData.Add(1014, new string[] { "기하학엔\n왕도가 없습니다.", "기하학!" });
        talkData.Add(1015, new string[] { "로고스 제일!", "충동을 따르는 삶을 \n살면...안 돼..." });
        talkData.Add(1016, new string[] { "아타락시아를,,,\n추구하는,,삶,,^^*", "무엇이 진정한 쾌락을 \n가져다 주는가...!" });
        talkData.Add(1017, new string[] { "누구지? 자네도 나의\n가르침을 얻으러 왔나?", "고소하겠어!" });

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


    //id에 맞춰서 적 이미지 활성화
    void EnemySetAct()
    {
        GameObject[] findEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < 17; i++)
        {
            if (get_Enemy_Id != findEnemy[i].GetComponent<NPCData>().id)
                findEnemy[i].SetActive(false);
        }
    }

    //id에 맞춰 적 스폐셜 공격 변경
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
