using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDownButton : MonoBehaviour
{
    public GameObject PopUp; //게임오브젝트 변수 선언
    public void NoButtonDown() //On Click() 함수
    {
        PopUp.SetActive(false); //startPopUp오브젝트 비활성화
    }
}
