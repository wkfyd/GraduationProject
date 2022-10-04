using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStartButton : MonoBehaviour
{
    private GameObject startPopUp;  //게임오브젝트 변수 선언
    public void NewStartButtonDown()  //On Click() 함수
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("Start_PopUp").gameObject;  //비활성화된 오브젝트참조
        startPopUp.SetActive(true);  //startPopUp오브젝트 비활성화
    }
}
