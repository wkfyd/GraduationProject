using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject start;
    public GameObject language;

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
}
