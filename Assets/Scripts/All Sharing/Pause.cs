using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{//B9AFA5
    public GameObject pause;

    public Sprite yes, no;
    public Image al, gre;

    public bool changeLanguage;
    bool isPause = false;

    void OnEnable()
    {
        isPause = true;
        Time.timeScale = 0;
        if (SaveData.isLanguage == 0)
        {
            al.sprite = yes;
            gre.sprite = no;
        }
        else
        {
            al.sprite = no;
            gre.sprite = yes;
        }
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
        al.sprite = yes;
        gre.sprite = no;

        if (SceneManager.GetActiveScene().name == "PuzzleStage" || SceneManager.GetActiveScene().name == "FreePuzStage")
            changeLanguage = true;
    }

    public void Greek()
    {
        SaveData.isLanguage = 1;
        Time.timeScale = 1;
        pause.SetActive(false);
        al.sprite = no;
        gre.sprite = yes;

        if (SceneManager.GetActiveScene().name == "PuzzleStage" || SceneManager.GetActiveScene().name == "FreePuzStage")
            changeLanguage = true;
    }
}
