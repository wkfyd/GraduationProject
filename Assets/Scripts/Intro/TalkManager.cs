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
        talkData.Add("��� �׸���, ��ũ��������.:0"); //����
        talkData.Add("���Ƿ罺 ������ �ѾƸ� �ȾƵ� û���� �ٱ��� �µ���\n���׳� ���� ���� �޸��� �ִ�.:0");
        talkData.Add("���� ����~~~!!! �̷��� �� ���潺�� �ʰھ�~~~@@!!:1");
        talkData.Add("��, �䡦���� �����̸� ���顦!!!\n���ƾ�!:1");
        talkData.Add("<size=70>��!!!</size>:0");
        talkData.Add("�ƾ߾ߡ�:1");
        talkData.Add("�̷�, ����������?\n�˼��մϴ�. ���� ���� �� �ó׿�:2");
        talkData.Add("�� ���Ƿ罺 ��� �� ����?\n���� ���� �� �ٺ��������� �����Կ�. �˼��մϴ�!:2");
        talkData.Add("��? ������?\n��ø���. ���� ������!!!!!!:1");
        talkData.Add("<size=70>�̰� �� �� �ƴϾ�!!!!!!!!!!!!!!!</size>:3");

        nameData.Add(nameArr[0]); //�� ����
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
