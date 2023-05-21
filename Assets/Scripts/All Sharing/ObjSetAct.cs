using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSetAct : MonoBehaviour
{
    public GameObject obj; //게임오브젝트 변수 선언
    public GameObject enemy_StartText;
    public GameObject player_text;

    public void objFalse() //On Click() 함수
    {
        obj.SetActive(false); //obj오브젝트 비활성화
    }

    public void objTrue()
    {
        obj.SetActive(true); //obj오브젝트 활성화
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
