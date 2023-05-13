using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //onClick �����
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadPuzzleStage01()
    {
        SceneManager.LoadScene("PuzzleStage01");
    }
}
