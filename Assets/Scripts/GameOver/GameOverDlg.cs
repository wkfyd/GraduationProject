using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverDlg : MonoBehaviour
{
    public GameOverText gameOverText;
    //���� ��ũ��Ʈ�� Ending��ũ��Ʈ�� ����
    public EndingTypeEffect talk;
    public Animator talkPanel;

    public GameObject fadeObj;


    public int talkIndex;

    void Start()
    {
        talk.SetMsg(gameOverText.GetTalk(0).Split(':')[0], talkIndex);
        talkIndex = 1;
    }

    public void Action()
    {
        Talk();
    }

    void Talk()
    {
        string talkData = gameOverText.GetTalk(talkIndex);

        //Ÿ����ȿ�����̸� return���� ����
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //�� ���� �־ ������ SetMsg�� �����ؼ� Ÿ���οϷḦ ���Ѿ��ϱ� ������
            return;
        }

        if (talkData == null)
        {
            StartCoroutine(FadeOutCorutine());
            return;
        }

        //��ȭâ �ִϸ��̼��� ������ ��� �� Ÿ�ָ̹°� �ؽ�Ʈ ����ֱ�����
        if (talkIndex == 2)
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

        //�̸� Image
        if (int.Parse(talkData.Split(':')[1]) == 0)
        {
            gameOverText.GetName(1).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 1)
        {
            gameOverText.GetName(int.Parse(talkData.Split(':')[1])).SetActive(true);
        }

        talkIndex++;
    }

    IEnumerator FadeOutCorutine()
    {
        fadeObj.SetActive(true);

        float count = 0;

        while (count <= 1.0f)
        {
            count += 0.01f;
            yield return new WaitForSeconds(0.02f);
            fadeObj.GetComponent<Image>().color = new Color(255, 255, 255, count);
        }

        SceneManager.LoadScene("GameTitle");
    }

    /*IEnumerator BookFadeInCorutine()
    {
        float count = 1;

        while (count >= 0)
        {
            count -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            bookFade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }*/
}
