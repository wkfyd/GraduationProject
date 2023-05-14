using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TypeEffect talk;
    public TalkManager talkManager;
    public Animator talkPanel;
    public Image portraitImg;

    public int talkIndex;

    void Start()
    {
        talk.SetMsg(talkManager.GetTalk(0).Split(':')[0]);
        talkIndex = 1;
    }

    public void Action()
    {
        Debug.Log(talkIndex);
        Talk();
    }

    void Talk()
    {
        string talkData = talkManager.GetTalk(talkIndex);

        //Ÿ����ȿ�����̸� return���� ����
        if (talk.isAnim)
        {
            talk.SetMsg(""); //�� ���� �־ ������ SetMsg�� �����ؼ� Ÿ���οϷḦ ���Ѿ��ϱ� ������
            return;
        }

        if (talkData == null)
        {
            SceneManager.LoadScene("Select Stage");
            return;
        }

        //��ȭâ �ִϸ��̼��� ������ ��� �� Ÿ�ָ̹°� �ؽ�Ʈ ����ֱ�����
        if (talkIndex == 2 || talkIndex == 4 || talkIndex == 5 || talkIndex == 6 || talkIndex == 8 ||talkIndex == 9)
        {
            talkPanel.SetTrigger("Talk Up And Down");
            StartCoroutine(TextTiming(talkData));
        }

        else
        {
            talk.SetMsg(talkData.Split(':')[0]);
            talkIndex++;
        }

    }

    IEnumerator TextTiming(string talkData)
    {
        yield return new WaitForSeconds(0.08f);

        talk.SetMsg(talkData.Split(':')[0]);

        //�ʻ�ȭ
        if (talkIndex == 2 || talkIndex == 3)
        {
            portraitImg.sprite = talkManager.GetPortrait(0);
            portraitImg.color = new Color(1, 1, 1, 1);

        }
        else if (talkIndex == 8 || talkIndex == 9)
        {
            portraitImg.sprite = talkManager.GetPortrait(1);
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        //�̸� Image
        if (int.Parse(talkData.Split(':')[1]) == 0)
        {
            talkManager.GetName(1).SetActive(false);
            talkManager.GetName(2).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 1)
        {
            talkManager.GetName(2).SetActive(false);
            talkManager.GetName(int.Parse(talkData.Split(':')[1])).SetActive(true);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 2)
        {
            talkManager.GetName(1).SetActive(false);
            talkManager.GetName(int.Parse(talkData.Split(':')[1])).SetActive(true);
        }

        talkIndex++;
    }
}
