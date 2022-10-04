using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YesButton : MonoBehaviour
{
    public void YesButtonDown() //On Click()함수
    {
        SceneManager.LoadScene("Select Stage"); //Select Stage씬으로 전환
    }
}
