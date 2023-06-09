using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    List<string> talkData;
    List<GameObject> nameData;

    public GameObject[] nameArr;

    void Awake()
    {
        talkData = new List<string>();
        nameData = new List<GameObject>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add("에피스테모노스는 치열한 사투 끝에 결국 패배하고 말았다.:0"); //……
        talkData.Add("발표 자료를 잃고 논문 디펜스 시간에도 지각한 그의 손에 남은 것은 공허함… \n공허함 뿐이었다.:0");

        //에피스테 말풍선 index == 2
        talkData.Add("이럴 순… 이럴 순 없어…… \n내게 한 번만 더 기회가 주어진다면…!:1");
        talkData.Add("크로노스여… 제발…!:1");

        //흰 색으로 페이드 아웃 index == 5
        //흰 화면에서 3초간 대기 후 (길면 1초) 타이틀 화면으로 이동하고 페이드 인

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

}
