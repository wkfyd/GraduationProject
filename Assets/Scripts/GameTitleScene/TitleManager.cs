using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject notSave;
    public GameObject newCollection;

    void Start()
    {
        newBook();
    }

    public void GameLoad()
    {
        if (SaveData.isTutorial == 1)
        {
            SaveData.GameLoad();
            SceneManager.LoadScene("Select Stage");
        }

        else
            notSave.SetActive(true);
    }

    public void newBook()
    {
        while (SaveData.newBook == 1)
        {
            newCollection.SetActive(false);
        }
    }
}
