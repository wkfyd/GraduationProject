using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSetAct : MonoBehaviour
{
    public GameObject obj; //���ӿ�����Ʈ ���� ����
    public GameObject enemy_StartText;
    public GameObject player_text;

    public void objFalse() //On Click() �Լ�
    {
        obj.SetActive(false); //obj������Ʈ ��Ȱ��ȭ
    }

    public void objTrue()
    {
        obj.SetActive(true); //obj������Ʈ Ȱ��ȭ
    }

    public void GameStart()
    {
        enemy_StartText.SetActive(true);
    }

    void TutorialText()
    {
        player_text.SetActive(true);
    }
}
