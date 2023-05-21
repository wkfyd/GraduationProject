using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFalse : MonoBehaviour
{
    public GameObject obj; //게임오브젝트 변수 선언

    public void objDown() //On Click() 함수
    {
        obj.SetActive(false); //obj오브젝트 비활성화
    }
}
