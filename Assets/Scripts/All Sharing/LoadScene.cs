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

    public void LoadPuzzleStage01()
    {
        SceneManager.LoadScene("PuzzleStage01");
    }

    public void LoadPuzzleStage02()
    {
        SceneManager.LoadScene("PuzzleStage02");
    }

    public void LoadPuzzleStage03()
    {
        SceneManager.LoadScene("PuzzleStage03");
    }

    public void LoadPuzzleStage04()
    {
        SceneManager.LoadScene("PuzzleStage04");
    }

    public void LoadPuzzleStage05()
    {
        SceneManager.LoadScene("PuzzleStage05");
    }

    public void LoadPuzzleStage06()
    {
        SceneManager.LoadScene("PuzzleStage06");
    }
}
