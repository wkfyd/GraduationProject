using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
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
        talkData.Add("고대 그리스, 아크로폴리스.:0"); //……
        talkData.Add("파피루스 묶음을 한아름 안아든 청년이 다급한 태도로\n아테나 신전 옆을 달리고 있다.:0");
        talkData.Add("지각 지각~~~!!! 이러다 논문 디펜스에 늦겠어~~~@@!!:1");
        talkData.Add("헉, 헉……이 모퉁이만 돌면…!!!\n으아악!:1");
        talkData.Add("<size=70>쿵!!!</size>:0");
        talkData.Add("아야야…:1");
        talkData.Add("이런, 괜찮으세요?\n죄송합니다. 제가 앞을 못 봤네요:2");
        talkData.Add("이 파피루스 당신 거 맞죠?\n제가 지금 좀 바빠서…빨리 가볼게요. 죄송합니다!:2");
        talkData.Add("어? 뭐라고요?\n잠시만요. 가지 마세요!!!!!!:1");
        talkData.Add("<size=70>이거 내 거 아니야!!!!!!!!!!!!!!!</size>:3");

        nameData.Add(nameArr[0]); //빈 옵젝
        nameData.Add(nameArr[1]); //PC_Name
        nameData.Add(nameArr[2]); //NPC_Name
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
