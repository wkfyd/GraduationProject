using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject start;
    public GameObject language;

    public GameObject enemy_StartText;
    public GameObject player_text;

    public bool tutorial;

    void Start()
    {
        if (!tutorial)
            language.SetActive(true);
        else
            start.SetActive(true);
    }

    public void SelectRomanLanguage()
    {
        language.SetActive(false);
        start.SetActive(true);

        Invoke("TutorialText", 1f);
    }

    public void SelectGreekLanguage()
    {
        language.SetActive(false);
        start.SetActive(true);

        Invoke("TutorialText", 1f);
    }

    public void GameStart()
    {
        start.SetActive(false);
        enemy_StartText.SetActive(true);

        StartCoroutine(EndTalk());
    }

    void TutorialText()
    {
        player_text.SetActive(true);
    }

    IEnumerator EndTalk()
    {
        yield return new WaitForSeconds(5.0f);

        enemy_StartText.SetActive(false);
    }
}
