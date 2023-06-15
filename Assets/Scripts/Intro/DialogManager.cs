using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TalkManager talkManager;
    public TypeEffect talk;
    public Animator talkPanel;
    public Image portraitImg;
    public CameraShake cameraShake;

    public GameObject pause;

    public Image textWindow;
    public Sprite textWindow_PC;
    public Sprite textWindow_else;

    public GameObject[] backGround;

    public int talkIndex;
    public int beforeIndex;

    public AudioSource audioSource;
    public AudioClip intro00;
    public AudioClip intro01;
    public AudioClip intro06;
    public AudioClip intro07;

    void Start()
    {
        audioSource.Play();
        talk.SetMsg(talkManager.GetTalk(0).Split(':')[0], talkIndex);
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
        string talkData = talkManager.GetTalk(talkIndex);

        //타이핑효과중이면 return으로 종료
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //빈 값을 넣어도 되지만 SetMsg를 실행해서 타이핑완료를 시켜야하기 때문에
            return;
        }

        if (talkData == null)
        {
            audioSource.Stop();
            SceneManager.LoadScene("Select Stage");
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
                break;

            case 6:
                audioSource.Stop();
                audioSource.PlayOneShot(intro06);
                break;

            case 7:
                audioSource.Stop();
                audioSource.PlayOneShot(intro07);
                break;

            case 8:
                audioSource.Stop();
                break;
        }

        //텍스트창 변경
        if (int.Parse(talkData.Split(':')[1]) == 1)
            textWindow.sprite = textWindow_PC;
        else
            textWindow.sprite = textWindow_else;

        //인트로 배경
        if (talkIndex == 5)
            backGround[0].SetActive(true);

        if (talkIndex == 8)
            backGround[1].SetActive(true);

        if (talkIndex == 9)
        {
            backGround[0].SetActive(false);
            backGround[1].SetActive(false);
        }

        //대화창 애니메이션이 있으면 재생 후 타이밍맞게 텍스트 띄워주기위해
        if (talkIndex == 2 ||  talkIndex == 5 || talkIndex == 6 || talkIndex == 8)
        {
            cameraShake.gameObject.SetActive(false);
            talkPanel.SetTrigger("Talk Up And Down");
            StartCoroutine(TextTiming(talkData, talkIndex));
        }

        else if (talkIndex == 4 || talkIndex == 9)
        {
            cameraShake.gameObject.SetActive(true);
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
        if (subTalkIndex == 2 || subTalkIndex == 3)
        {
            portraitImg.sprite = talkManager.GetPortrait(0);
            portraitImg.color = new Color(1, 1, 1, 1);

        }
        else if (subTalkIndex == 9)
        {
            portraitImg.sprite = talkManager.GetPortrait(1);
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        //이름 Image
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

    public void PlaySound(AudioClip soundClip, float volume)
    {
        Debug.Log("Sound played: " + soundClip);

        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = soundClip;
        audioSource.Play();

        // 사운드 재생이 끝나면 게임 오브젝트 파괴
        Destroy(soundObject, soundClip.length);
    }

    public void StopSound(AudioClip soundClip)
    {
        Destroy(soundClip);
    }
}
