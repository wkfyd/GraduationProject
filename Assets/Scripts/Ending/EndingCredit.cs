using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingCredit : MonoBehaviour
{
    public TextMeshProUGUI endingText;

    public float scrollSpeed = 100f;

    RectTransform creditsRectTransform;

    void Start()
    {
        creditsRectTransform = GameObject.Find("EndingCredit").GetComponent<RectTransform>();
        StartCoroutine(EndingText());
    }

    IEnumerator ScrollCredits()
    {
        float scrollPosition = -6400.0f;
        float targetPosition = 5000f;

        while (scrollPosition < targetPosition)
        {
            scrollPosition += scrollSpeed * Time.deltaTime;
            creditsRectTransform.anchoredPosition = new Vector3(creditsRectTransform.anchoredPosition.x, scrollPosition, 0);

            yield return null;
        }
    }

    IEnumerator EndingText()
    {
        yield return ScrollCredits();

        yield return new WaitForSeconds(2f);

        yield return EndingFadeIn();

        yield return new WaitForSeconds(2f);

        yield return EndingFadeOut();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameTitle");
    }

    IEnumerator EndingFadeIn()
    {
        float fadeCount = 0;
        float speed = 0.1f;

        while (fadeCount < 1.0f)
        {
            fadeCount += speed * Time.deltaTime;
            endingText.color = new Color(1, 1, 1, fadeCount);
            yield return null;
        }
    }

    IEnumerator EndingFadeOut()
    {
        float fadeCount = 1;
        float speed = 0.5f;

        while (fadeCount > 0)
        {
            fadeCount -= speed * Time.deltaTime;
            endingText.color = new Color(1, 1, 1, fadeCount);
            yield return null;
        }
    }
}
