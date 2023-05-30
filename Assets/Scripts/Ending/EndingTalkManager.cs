using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTalkManager : MonoBehaviour
{
    List<string> talkData;
    List<GameObject> nameData;

    public GameObject[] nameArr;
    public Sprite[] portraitImg;

    void Awake()
    {
        talkData = new List<string>();
        nameData = new List<GameObject>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add("드디어 찾았다. 내 발표자료! 당신이 가지고 갔었구나!!!\n헉, 헉… 빨리 발표하러 가야 되는데! 지금, 지금 몇 시지?!:1");
        talkData.Add("아… 아슬아슬하지만 괜찮겠어!:1");
        talkData.Add("하하핫!!!!!! 아하핫… 아핫핫하하하핫!!!!!!!!!!\n나의 승리다… 시간의 신 크로노스여………!!!!!!!!!!:1");
        talkData.Add("아하하핫핫핫………!!!\n기다려 주세요 교수님들! 제가 갑니다!!!:1");
        talkData.Add("이렇게 무사히 발표 자료를 되찾은 에피스테모노스는\n 논문 심사에 통과해 박사 학위를 따고:0");
        talkData.Add("몇 년 뒤 자신의 이름을 딴 에피스테모노스 학파를 만들어\n사람들을 가르쳤다고 한다.:0");
        talkData.Add("오오, 에피스테모노스여.\n그 이름 길이 기억되리라………:0");

        nameData.Add(nameArr[0]); //빈 옵젝
        nameData.Add(nameArr[1]); //PC_Name
    }

    public string GetTalk(int talkIndex)
    {
        if (talkIndex == talkData.Count)
            return null;
        else
            return talkData[talkIndex];
    }

    public GameObject GetName(int nameIndex)
    {
        return nameData[nameIndex];
    }

    public Sprite GetPortrait(int portraitIndex)
    {
        return portraitImg[portraitIndex];
    }
}
