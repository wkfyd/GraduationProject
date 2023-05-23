using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject start;
    public GameObject language;

    public GameObject player_text;
    public GameObject[] tuto_text;

    public bool tutorial;
    public bool tuto_merge;

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

    void TutorialText()
    {
        player_text.SetActive(true);
    }

    public void TutoTextLast()
    {
        tuto_text[0].SetActive(false);
        tuto_text[1].SetActive(true);
        Invoke("falseText", 4f);
    }

    void falseText()
    {
        player_text.SetActive(false);
    }
}
