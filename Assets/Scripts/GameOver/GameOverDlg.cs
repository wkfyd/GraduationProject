using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDlg : MonoBehaviour
{
    public GameOverText gameOverText;
    //같은 스크립트라 Ending스크립트꺼 공유
    public EndingTypeEffect talk;
    public Animator talkPanel;

    public GameObject fadeObj;

    public Image textWindow;
    public Sprite textWindow_PC;

    public int talkIndex;

    public AudioSource audioSource;
    public AudioClip intro00;
    public AudioClip intro01;
    public AudioClip intro02;
    public AudioClip intro03;

    void Start()
    {
        audioSource.Play();
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

        //타이핑효과중이면 return으로 종료
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //빈 값을 넣어도 되지만 SetMsg를 실행해서 타이핑완료를 시켜야하기 때문에
            return;
        }

        if (talkData == null)
        {
            audioSource.Stop();
            StartCoroutine(FadeOutCorutine());
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
        }

        //대화창 애니메이션이 있으면 재생 후 타이밍맞게 텍스트 띄워주기위해
        if (talkIndex == 2)
        {
            textWindow.sprite = textWindow_PC;
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

        //이름 Image
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
}
