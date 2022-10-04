using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    private GameObject startPopUp; //게임오브젝트 변수 선언
    public void NoButtonDown() //On Click() 함수
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("Start_PopUp").gameObject; //비활성화된 오브젝트참조
        startPopUp.SetActive(false); //startPopUp오브젝트 비활성화
    }
}
