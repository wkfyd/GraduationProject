using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulManager : MonoBehaviour
{
    public SimulDataManager SimulDataManager;
    public SimulTypeEffect talk;

    public Animator talkPanel;
    public GameObject talkSet;
    public GameObject portraitImg;
    public GameObject camera;

    public GameObject choice;
    public TextMeshProUGUI choice_01;
    public TextMeshProUGUI choice_02;

    public TextMeshProUGUI enemyName;
    public int enemyId;
    public int talkIndex;
    int talkingChar = 0;
    int talkingChar_Next = 0;

    public GameObject simulFade;
    public GameObject bookFade;

    public SpriteRenderer bg;
    public Sprite[] bgSource;

    public GameObject[] otherCanvas;
    public GameObject simulCanvas;
    public GameObject logo;

    public void Action()
    {
        Talk(enemyId);
    }

    void Talk(int enemyId)
    {
        string talkData = SimulDataManager.GetTalk(enemyId, talkIndex);


        //타이핑효과중이면 return으로 종료
        if (talk.isAnim)
        {
            talk.SetMsg(""); //빈 값을 넣어도 되지만 SetMsg를 실행해서 타이핑완료를 시켜야하기 때문에
            return;
        }

        //종료
        if (talkData == null)
        {
            StartCoroutine(BookOn());
            StartCoroutine(SimulFadeOutCorutine());
            return;
        }

        //소크라테스
        if (enemyId == 1001)
        {
            /*talkingChar_Next = int.Parse(talkData.Split(':')[1]);

            //화자 변경
            if (talkingChar != talkingChar_Next)
            {
                talkPanel.SetTrigger("Talk Up And Down");
                StartCoroutine(TextTiming(talkData));
                talkingChar = talkingChar_Next;
            }*/

            //일러스트 등장
            if(talkIndex == 4)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //선택 분기
            else if (talkIndex == 8 || talkIndex == 16 || talkIndex == 40 || talkIndex == 46)
            {
                Branch_Socra();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //일러스트 삭제
            else if(talkIndex == 52 || talkIndex == 60)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //카메라 흔들림
            else if (talkIndex == 57 || talkIndex == 58)
            {
                StartCoroutine(CameraShake());
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            else
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            

        }

        //이름텍스트
        if (int.Parse(talkData.Split(':')[1]) == 0)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 1)
        {
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(1).SetActive(true);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 2)
        {
            SimulDataManager.GetName(2).SetActive(true);
            SimulDataManager.GetName(1).SetActive(false);
        }
    }

    /*IEnumerator TextTiming(string talkData)
    {
        yield return new WaitForSeconds(0.08f);

        talk.SetMsg(talkData.Split(':')[0]);

        talkIndex++;
    }*/

    public void TalkButton_Socra()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1001;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "소크라테스";
    }

    public void Branch_Socra()
    {
        if(talkIndex == 8)
        {
            choice.SetActive(true);
            choice_01.text = "당연하죠?";
            choice_02.text = "아뇨…?";
        }

        else if(talkIndex == 16)
        {
            choice.SetActive(true);
            choice_01.text = "사람들이 왜 당신을 싫어하는지 알 것 같아요.";
            choice_02.text = "소크라테스 씨는 아는 게 많군요.";
        }

        else if(talkIndex == 40)
        {
            choice.SetActive(true);
            choice_01.text = "동의한다";
            choice_02.text = "동의하지 않는다";
        }

        else if (talkIndex == 46)
        {
            choice.SetActive(true);
            choice_01.text = "수락한다";
            choice_02.text = "거절한다";
        }
    }
    public void Choice_Socra_Up()
    {
        choice.SetActive(false);

        if (talkIndex == 8)
        {
            talkIndex = 9;
            Talk(enemyId);
        }

        else if (talkIndex == 16)
        {
            talkIndex = 17;
            Talk(enemyId);
        }
            
        else if (talkIndex == 40)
        {
            talkIndex = 41;
            Talk(enemyId);
        }
            
        else if (talkIndex == 46)
        {
            talkIndex = 47;
            Talk(enemyId);
        }
            
    }

    public void Choice_Socra_Down()
    {
        choice.SetActive(false);

        if (talkIndex == 8)
        {
            talkIndex = 13;
            Talk(enemyId);
        }

        else if (talkIndex == 16)
        {
            talkIndex = 33;
            Talk(enemyId);
        }

        else if (talkIndex == 40)
        {
            talkIndex = 43;
            Talk(enemyId);
        }

        else if (talkIndex == 46)
        {
            talkIndex = 54;
            Talk(enemyId);
        }
    }

    IEnumerator CameraShake()
    {
        camera.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        camera.SetActive(false);
    }

    IEnumerator SimulOn()
    {
        simulFade.SetActive(true);
        bookFade.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        otherCanvas[0].SetActive(false);
        otherCanvas[1].SetActive(false);
        simulCanvas.SetActive(true);
        logo.SetActive(false);
        bg.sprite = bgSource[1];

        StartCoroutine(SimulFadeInCorutine());

        yield return new WaitForSeconds(2.0f);

        simulFade.SetActive(false);
        bookFade.SetActive(false);
        talkSet.SetActive(true);

        talk.SetMsg(SimulDataManager.GetTalk(1001, 0));
        talkIndex = 1;
    }

    IEnumerator BookOn()
    {
        simulFade.SetActive(true);
        bookFade.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        otherCanvas[0].SetActive(true);
        otherCanvas[1].SetActive(true);
        simulCanvas.SetActive(false);
        logo.SetActive(true);
        bg.sprite = bgSource[0];

        StartCoroutine(BookFadeInCorutine());

        yield return new WaitForSeconds(2.0f);

        enemyId = 0;

        simulFade.SetActive(false);
        bookFade.SetActive(false);
    }

    IEnumerator BookFadeOutCorutine()
    {
        float count = 0;

        while (count <= 1.0f)
        {
            count += 0.01f;
            yield return new WaitForSeconds(0.02f);
            bookFade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }

    IEnumerator BookFadeInCorutine()
    {
        float count = 1;

        while (count >= 0)
        {
            count -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            bookFade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }

    IEnumerator SimulFadeOutCorutine()
    {
        float count = 0;

        while (count <= 1.0f)
        {
            count += 0.01f;
            yield return new WaitForSeconds(0.02f);
            simulFade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }

    IEnumerator SimulFadeInCorutine()
    {
        float count = 1;

        while (count >= 0)
        {
            count -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            simulFade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }
}
