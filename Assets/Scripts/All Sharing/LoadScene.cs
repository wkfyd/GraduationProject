using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSelectStageScene() //On Click()함수
    {
        SceneManager.LoadScene("Select Stage"); //Select Stage씬으로 전환
    }
    public void LoadPuzzleStage01() //On Click()함수
    {
        SceneManager.LoadScene("PuzzleStage01"); //Select Stage씬으로 전환
    }
}
