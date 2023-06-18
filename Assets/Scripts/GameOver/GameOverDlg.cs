using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDlg : MonoBehaviour
{
    public GameOverText gameOverText;
    //���� ��ũ��Ʈ�� Ending��ũ��Ʈ�� ����
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

        //Ÿ����ȿ�����̸� return���� ����
        if (talk.isAnim)
        {
            talk.SetMsg("", talkIndex); //�� ���� �־ ������ SetMsg�� �����ؼ� Ÿ���οϷḦ ���Ѿ��ϱ� ������
            return;
        }

        if (talkData == null)
        {
            audioSource.Stop();
            StartCoroutine(FadeOutCorutine());
            return;
        }

        //����
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

        //��ȭâ �ִϸ��̼��� ������ ��� �� Ÿ�ָ̹°� �ؽ�Ʈ ����ֱ�����
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
}
