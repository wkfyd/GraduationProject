using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //onClick �����
    public void NewPlay()
    {
        PlayerPrefs.DeleteAll();

        SaveData.NewPlay();

        SceneManager.LoadScene("Intro");
    }

    public void LoadPuzzleStage()
    {
        SceneManager.LoadScene("PuzzleStage");
    }

    public void LoadFreeStage()
    {
        SceneManager.LoadScene("FreePuzStage");
    }

    public void LoadGameWin()
    {
        if (SaveData.currentStage == 6 && SaveData.isEnding == 0)
        {
            SaveData.isEnding = 1;
            SceneManager.LoadScene("Ending");

            SaveData.currentStage++;

            if (SaveData.currentStage == 7)
                SaveData.currentStage = 1;
        }
            
        else
        {
            //óġ �� �������� ����, 6�������������� 1����������
            SaveData.currentStage++;

            if (SaveData.currentStage == 7)
                SaveData.currentStage = 1;

            SceneManager.LoadScene("Select Stage");
        }

        Time.timeScale = 1;
    }

    public void LoadGameOver()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameOver");
    }

    public void LoadSelectStage()
    {
        SceneManager.LoadScene("Select Stage");
    }
}
