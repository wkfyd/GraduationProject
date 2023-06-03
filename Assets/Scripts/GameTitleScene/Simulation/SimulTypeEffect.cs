using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulTypeEffect : MonoBehaviour
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

    public void SetMsg(string msg)
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
            EffectStart();
        }
    }

    void EffectStart()
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
