using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFalse : MonoBehaviour
{
    public GameObject obj; //���ӿ�����Ʈ ���� ����

    public void objDown() //On Click() �Լ�
    {
        obj.SetActive(false); //obj������Ʈ ��Ȱ��ȭ
    }
}
