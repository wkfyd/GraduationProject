using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject notSave;
    public GameObject newCollection;
    public GameObject titleFade;

    void Start()
    {
        if (SaveData.isGameOver == 1)
        {
            StartCoroutine(TitleFadeInCorutine());
        }

        SaveData.GameLoad();
        newBook();
    }

    public void GameLoad()
    {
        if (SaveData.isTutorial == 1)
        {
            SceneManager.LoadScene("Select Stage");
        }

        else
            notSave.SetActive(true);
    }

    public void newBook()
    {
        if (SaveData.newBook == 1)
        {
            newCollection.SetActive(true);
        }
    }


    IEnumerator TitleFadeInCorutine()
    {
        titleFade.SetActive(true);

        float count = 1;

        while (count >= 0)
        {
            count -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            titleFade.GetComponent<Image>().color = new Color(255, 255, 255, count);
        }

        SaveData.isGameOver = 0;
        titleFade.SetActive(false);
    }
}
