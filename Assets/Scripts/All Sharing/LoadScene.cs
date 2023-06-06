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

    public void LoadGameWin()
    {
        SceneManager.LoadScene("PuzzleStage");

        if(SaveData.currentStage == 6 && SaveData.ending == 0)
            SceneManager.LoadScene("Ending");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
