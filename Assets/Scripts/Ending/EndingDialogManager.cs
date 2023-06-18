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

    public GameObject pause;

    public Image textWindow;
    public Sprite textWindow_else;

    public int talkIndex;

    public AudioSource audioSource;
    public AudioClip intro00;
    public AudioClip intro01;
    public AudioClip intro02;
    public AudioClip intro03;
    public AudioClip intro04;
    public AudioClip intro05;
    public AudioClip intro06;

    void Start()
    {
        SaveData.isEnding = 1;

        //엔딩 세이브
        SaveData.GameSave();

        audioSource.Play();
        talk.SetMsg(endingTM.GetTalk(0).Split(':')[0], talkIndex);
        talkIndex = 1;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause.SetActive(true);
    }

    public void Action()
    {
        Talk();
    }

    void Talk()
    {
        string talkData = endingTM.GetTalk(talkIndex);

        //타이핑효과중이면 return으로 종료
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //빈 값을 넣어도 되지만 SetMsg를 실행해서 타이핑완료를 시켜야하기 때문에
            return;
        }

        if (talkData == null)
        {
            audioSource.Stop();
            SceneManager.LoadScene("EndingCredit");
            return;
        }

        //더빙
        switch (talkIndex)
        {
            case 1:
                audioSource.Stop();
                audioSource.PlayOneShot(intro01);
                break;

            case 2:
                audioSource.Stop();
                audioSource.PlayOneShot(intro02);
                break;

            case 3:
                audioSource.Stop();
                audioSource.PlayOneShot(intro03);
                break;

            case 4:
                audioSource.Stop();
                audioSource.PlayOneShot(intro04);
                break;

            case 5:
                audioSource.Stop();
                audioSource.PlayOneShot(intro05);
                break;

            case 6:
                audioSource.Stop();
                audioSource.PlayOneShot(intro06);
                break;
        }


        if (talkIndex == 2)
            StartCoroutine(TextTiming(talkData, talkIndex));

        else if (talkIndex == 4)
        {
            textWindow.sprite = textWindow_else;
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

        //초상화 talkManager -> PortraitImg[]
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

        //이름 Image
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
