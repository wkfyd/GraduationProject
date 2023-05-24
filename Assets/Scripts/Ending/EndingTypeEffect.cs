using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingTypeEffect : MonoBehaviour
{
    public GameObject endCursor;

    TextMeshProUGUI msgText;

    public int CharPerSeconds;
    public bool isAnim;

    string targetMsg;
    int index;
    float interval;

    void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg, int talkIndex)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;

            CancelInvoke();

            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart(talkIndex);
        }
    }

    void EffectStart(int talkIndex)
    {
        msgText.text = "";

        index = 0;
        endCursor.SetActive(false);

        isAnim = true;

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
        isAnim = false;
        endCursor.SetActive(true);
    }
}
