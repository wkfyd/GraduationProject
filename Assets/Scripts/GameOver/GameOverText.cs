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
        talkData.Add("���ǽ��׸�뽺�� ġ���� ���� ���� �ᱹ �й��ϰ� ���Ҵ�.:0"); //����
        talkData.Add("��ǥ �ڷḦ �Ұ� �� ���潺 �ð����� ������ ���� �տ� ���� ���� �����ԡ� \n������ ���̾���.:0");

        //���ǽ��� ��ǳ�� index == 2
        talkData.Add("�̷� ���� �̷� �� ����� \n���� �� ���� �� ��ȸ�� �־����ٸ顦!:1");
        talkData.Add("ũ�γ뽺���� ���ߡ�!:1");

        //�� ������ ���̵� �ƿ� index == 5
        //�� ȭ�鿡�� 3�ʰ� ��� �� (��� 1��) Ÿ��Ʋ ȭ������ �̵��ϰ� ���̵� ��

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

}
