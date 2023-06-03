using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void SkipButtonDown()  //On Click() 함수
    {
        //인트로 씬 스킵 시
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            SceneManager.LoadScene("Select Stage");
        }

        //엔딩 씬 스킵 시
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            SceneManager.LoadScene("EndingCredit");
        }

        //엔딩 크레딧 스킵 시
        if (SceneManager.GetActiveScene().name == "EndingCredit")
        {
            SceneManager.LoadScene("GameTitle");
        }

        //게임 오버 씬 스킵 시
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            SceneManager.LoadScene("GameTitle");
        }

        //미연시 스킵 시
        if (SceneManager.GetActiveScene().name == "Simulation") /* && [어떤 캐릭터인지 판별 가능한 조건] */
        {
            SceneManager.LoadScene("GameTitle"); /*&& 캐릭터 상세 정보 화면이 띄워져 있어야 합니다*/
        }
    }
}
