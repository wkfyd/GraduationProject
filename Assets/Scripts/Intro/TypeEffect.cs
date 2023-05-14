using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public GameObject endCursor;

    TextMeshProUGUI msgText;

    public int CharPerSeconds;
    string targetMsg;
    int index;
    float interval;

    void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        endCursor.SetActive(false);

        interval = 1.0f / CharPerSeconds;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        endCursor.SetActive(true);
    }
}
