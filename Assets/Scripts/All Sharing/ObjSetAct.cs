using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjSetAct : MonoBehaviour
{
    public GameObject obj; //���ӿ�����Ʈ ���� ����

    public void objFalse() //On Click() �Լ�
    {
        obj.SetActive(false); //obj������Ʈ ��Ȱ��ȭ
    }

    public void objTrue()
    {
        obj.SetActive(true); //obj������Ʈ Ȱ��ȭ
    }
}
