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
        talkData.Add("���� ã�Ҵ�. �� ��ǥ�ڷ�! ����� ������ ��������!!!\n��, �䡦 ���� ��ǥ�Ϸ� ���� �Ǵµ�! ����, ���� �� ����?!:1");
        talkData.Add("�ơ� �ƽ��ƽ������� �����ھ�!:1");
        talkData.Add("������!!!!!! �����֡� ��������������!!!!!!!!!!\n���� �¸��١� �ð��� �� ũ�γ뽺��������!!!!!!!!!!:1");
        talkData.Add("�����������֡�����!!!\n��ٷ� �ּ��� �����Ե�! ���� ���ϴ�!!!:1");
        talkData.Add("�̷��� ������ ��ǥ �ڷḦ ��ã�� ���ǽ��׸�뽺��\n �� �ɻ翡 ����� �ڻ� ������ ����:0");
        talkData.Add("�� �� �� �ڽ��� �̸��� �� ���ǽ��׸�뽺 ���ĸ� �����\n������� �����ƴٰ� �Ѵ�.:0");
        talkData.Add("����, ���ǽ��׸�뽺��.\n�� �̸� ���� ���Ǹ��󡦡���:0");

        nameData.Add(nameArr[0]); //�� ����
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
