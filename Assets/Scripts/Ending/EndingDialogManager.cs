using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingDialogManager : MonoBehaviour
{
    public EndingTalkManager endingTM;
    public EndingTypeEffect talk;
    public Animator talkPanel;
    public Image portraitImg;

    public int talkIndex;
    void Start()
    {
        SaveData.ending = 1;

        talk.SetMsg(endingTM.GetTalk(0).Split(':')[0], talkIndex);
        talkIndex = 1;
        
    }

    public void Action()
    {
        Talk();
    }

    void Talk()
    {
        string talkData = endingTM.GetTalk(talkIndex);

        //Ÿ����ȿ�����̸� return���� ����
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //�� ���� �־ ������ SetMsg�� �����ؼ� Ÿ���οϷḦ ���Ѿ��ϱ� ������
            return;
        }

        if (talkData == null)
        {
            SceneManager.LoadScene("EndingCredit");
            return;
        }

        if (talkIndex == 2)
            StartCoroutine(TextTiming(talkData, talkIndex));

        else if (talkIndex == 4)
        {
            talkPanel.SetTrigger("Talk Up And Down");
            StartCoroutine(TextTiming(talkData, talkIndex));
        }

        else
        {
            talk.SetMsg(talkData.Split(':')[0], talkIndex);
            talkIndex++;
        }
    }

    IEnumerator TextTiming(string talkData, int subTalkIndex)
    {
        yield return new WaitForSeconds(0.08f);

        talk.SetMsg(talkData.Split(':')[0], subTalkIndex);

        //�ʻ�ȭ talkManager -> PortraitImg[]
        if (subTalkIndex == 0 || subTalkIndex == 1)
        {
            portraitImg.sprite = endingTM.GetPortrait(0);
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else if (subTalkIndex == 2 || subTalkIndex == 3)
        {
            portraitImg.sprite = endingTM.GetPortrait(1);
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        //�̸� Image
        if (int.Parse(talkData.Split(':')[1]) == 0)
        {
            endingTM.GetName(1).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 1)
        {
            endingTM.GetName(1).SetActive(true);
        }

        talkIndex++;
    }
}
