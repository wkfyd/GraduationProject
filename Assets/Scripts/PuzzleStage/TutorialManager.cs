using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject start;
    public GameObject language;

    public GameObject player_text;
    public GameObject[] tuto_text;

    public bool tuto_merge;


    void Start()
    {
        if (SaveData.isTutorial != 1)
            language.SetActive(true);
        else
            start.SetActive(true);
    }

    public void SelectRomanLanguage()
    {
        language.SetActive(false);
        start.SetActive(true);

        SaveData.isLanguage = 0;
    }

    public void SelectGreekLanguage()
    {
        language.SetActive(false);
        start.SetActive(true);

        SaveData.isLanguage = 1;
    }
}
