using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause;

    bool isPause = false;

    void OnEnable()
    {
        isPause = true;
        Time.timeScale = 0;
    }

    public void FreeStopBtn()
    {
        if (SceneManager.GetActiveScene().name == "FreePuzStage")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Select Stage");
        }

        if (SceneManager.GetActiveScene().name == "Intro")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameTitle");
        }

        if (SceneManager.GetActiveScene().name == "Select Stage")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameTitle");
        }

        if (SceneManager.GetActiveScene().name == "PuzzleStage")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Select Stage");
        }

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("EndingCredit");
        }
    }

    public void KeepBtn()
    {
        isPause = false;
        Time.timeScale = 1;
        pause.SetActive(false);
    }

    public void Alpha()
    {
        SaveData.isLanguage = 0;
        Time.timeScale = 1;
        pause.SetActive(false);
    }

    public void Greek()
    {
        SaveData.isLanguage = 1;
        Time.timeScale = 1;
        pause.SetActive(false);
    }
}
