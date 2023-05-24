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

    public void LoadEnding()
    {
        SceneManager.LoadScene("Ending");
    }

    public void LoadFreeStage()
    {
        SceneManager.LoadScene("PuzzleStage");
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
