using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //onClick »ç¿ëÁß
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("Intro");
    }

    public void LoadPuzzleStage()
    {
        SceneManager.LoadScene("PuzzleStage");
    }

    public void LoadFreeStage()
    {
        SceneManager.LoadScene("PuzzleStage");
    }
}
