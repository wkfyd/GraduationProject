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
    public Image textWindow;
    public Sprite textWindow_PC;
    public Sprite textWindow_else;
    public GameObject portraitImg;
    public GameObject extraImg_01;
    public GameObject extraImg_02;
    public GameObject extraImg_03;
    public Sprite archi_Change;
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
    public GameObject Thales_Fade;

    public SpriteRenderer bg;
    public Sprite[] bgSource;

    public GameObject[] otherCanvas;
    public GameObject simulCanvas;
    public GameObject logo;
    public GameObject titleEffect;

    public void Action()
    {
        Talk(enemyId);
    }

    void Talk(int enemyId)
    {
        string talkData = SimulDataManager.GetTalk(enemyId, talkIndex);

        //=========================전체적용=================================

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

        //화자 변경
        talkingChar_Next = int.Parse(talkData.Split(':')[1]);

        if ((talkingChar == 0 || talkingChar == 1) && talkingChar_Next == 2 ||
             talkingChar == 2 && (talkingChar_Next == 1 || talkingChar_Next == 0))
        {
            talkPanel.SetTrigger("Talk Up And Down");
            talkingChar = talkingChar_Next;
        }

        //텍스트창 변경
        if (int.Parse(talkData.Split(':')[1]) == 1)
            textWindow.sprite = textWindow_PC;
        else
            textWindow.sprite = textWindow_else;

        //이름박스
        if (int.Parse(talkData.Split(':')[1]) == 0)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(3).SetActive(false);
            SimulDataManager.GetName(4).SetActive(false);
            SimulDataManager.GetName(5).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 1)
        {
            SimulDataManager.GetName(1).SetActive(true);
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(3).SetActive(false);
            SimulDataManager.GetName(4).SetActive(false);
            SimulDataManager.GetName(5).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 2)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(true);
            SimulDataManager.GetName(3).SetActive(false);
            SimulDataManager.GetName(4).SetActive(false);
            SimulDataManager.GetName(5).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 3)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(3).SetActive(true);
            SimulDataManager.GetName(4).SetActive(false);
            SimulDataManager.GetName(5).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 4)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(3).SetActive(false);
            SimulDataManager.GetName(4).SetActive(true);
            SimulDataManager.GetName(5).SetActive(false);
        }

        else if (int.Parse(talkData.Split(':')[1]) == 5)
        {
            SimulDataManager.GetName(1).SetActive(false);
            SimulDataManager.GetName(2).SetActive(false);
            SimulDataManager.GetName(3).SetActive(false);
            SimulDataManager.GetName(4).SetActive(false);
            SimulDataManager.GetName(5).SetActive(true);
        }

 //============================소크라테스=================================
        if (enemyId == 1001)
        {
            //일러스트 등장
            if (talkIndex == 4)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 52 || talkIndex == 60)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //선택 분기
            else if (talkIndex == 8 || talkIndex == 16 || talkIndex == 32 || talkIndex == 40 || talkIndex == 46)
            {
                Branch_Socra();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //윗답변을 선택시에 아랫답도 같이 나오기때문에 인덱스 변경
            else if (talkIndex == 12)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 16;
            }

            else if (talkIndex == 32)
            {
                talk.SetMsg(talkData.Split(':')[0]);
            }

            else if (talkIndex == 42)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 45;
            }

            else if (talkIndex == 53)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 61;
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

//=============================플라톤====================================
        if (enemyId == 1002)
        {
            //일러스트 등장
            if (talkIndex == 1)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 50)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //선택 분기
            else if (talkIndex == 6 || talkIndex == 18 || talkIndex == 24)
            {
                Branch_Plato();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //윗답변을 선택시에 아랫답도 같이 나오기때문에 인덱스 변경
            else if (talkIndex == 10)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 16;
            }

            else if (talkIndex == 28)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 31;
            }

            else if (talkIndex == 32)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 36;
            }

            else
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }
        }

//===============================아리스토텔레스=====================================
        if (enemyId == 1003)
        {
            //일러스트 등장
            if (talkIndex == 1 || talkIndex == 11 || talkIndex == 18)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 8 || talkIndex == 16 || talkIndex == 33)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //카메라 흔들림
            else if (talkIndex == 6)
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

//==============================피타고라스=========================================
        if (enemyId == 1004)
        {
            //일러스트 등장
            if (talkIndex == 4 || talkIndex == 47)
            {
                portraitImg.SetActive(true);

                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 33 || talkIndex == 45)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //엑스트라 등장
            else if (talkIndex == 5 || talkIndex == 34)
            {
                portraitImg.SetActive(false);
                extraImg_01.SetActive(true);
                extraImg_02.SetActive(true);

                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            else if (talkIndex == 49)
            {
                portraitImg.SetActive(false);
                extraImg_03.SetActive(true);

                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //엑스트라 삭제
            else if (talkIndex == 8)
            {
                portraitImg.SetActive(true);
                extraImg_01.SetActive(false);
                extraImg_02.SetActive(false);

                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            else if (talkIndex == 40)
            {
                extraImg_01.SetActive(false);
                extraImg_02.SetActive(false);

                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //배경변경
            else if (talkIndex == 43)
            {
                StartCoroutine(ChangeBG_Pytha());
            }

            //선택 분기
            else if (talkIndex == 14)
            {
                Branch_Pytha();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //윗답변을 선택시에 아랫답도 같이 나오기때문에 인덱스 변경
            else if (talkIndex == 16)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 20;
            }

            //카메라 흔들림
            else if (talkIndex == 3)
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

//==============================아르키메데스=========================================
        if (enemyId == 1005)
        {
            //일러스트 등장
            if (talkIndex == 8)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 38 || talkIndex == 55)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 교체
            else if (talkIndex == 51)
            {
                portraitImg.GetComponent<Image>().sprite = archi_Change;
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //선택 분기
            else if (talkIndex == 10 || talkIndex == 33)
            {
                Branch_Archi();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //윗답변을 선택시에 아랫답도 같이 나오기때문에 인덱스 변경
            else if (talkIndex == 12)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 17;
            }

            else if (talkIndex == 39)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 56;
            }

            //카메라 흔들림
            else if (talkIndex == 4 || talkIndex == 15 || talkIndex == 19 || talkIndex == 24 || talkIndex == 38)
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

 //=====================================탈레스====================================
        if (enemyId == 1006)
        {
            //일러스트 등장
            if (talkIndex == 6)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 35)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //배경변경
            else if (talkIndex == 5 || talkIndex == 36)
                StartCoroutine(ChangeBG_Thales());

            //페이드아웃
            else if (talkIndex == 41)
            {
                Thales_Fade.SetActive(true);
                StartCoroutine(ThalesFadeOutCorutine());
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //카메라 흔들림
            else if (talkIndex == 9)
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

//==================================에피쿠로스======================================
        if (enemyId == 1007)
        {
            //일러스트 등장
            if (talkIndex == 12)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 34)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //선택 분기
            else if (talkIndex == 16)
            {
                Branch_Epicuru();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //윗답변을 선택시에 아랫답도 같이 나오기때문에 인덱스 변경
            else if (talkIndex == 20)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 25;
            }

            else
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }
        }

//================================키티움의 제논===================================
        if (enemyId == 1008)
        {
            //일러스트 등장
            if (talkIndex == 12 || talkIndex == 30)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //일러스트 삭제
            else if (talkIndex == 16 || talkIndex == 33)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //카메라 흔들림
            else if (talkIndex == 14)
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
    }

    public void TalkButton_Socra()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1001;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "소크라테스";
    }

    public void TalkButton_Plato()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1002;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "플라톤";
    }

    public void TalkButton_Aristo()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1003;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "아리스토텔레스";
    }

    public void TalkButton_Pytha()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1004;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "피타고라스";
    }

    public void TalkButton_Archi()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1005;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "아르키메데스";
    }

    public void TalkButton_Thales()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1006;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "탈레스";
    }

    public void TalkButton_Epicuru()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1007;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "에피쿠로스";
    }

    public void TalkButton_Zeno()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1008;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "제논";
    }

    public void Branch_Socra()
    {
        if (talkIndex == 8)
        {
            choice.SetActive(true);
            choice_01.text = "당연하죠?";
            choice_02.text = "아뇨…?";
        }

        else if (talkIndex == 16)
        {
            choice.SetActive(true);
            choice_01.text = "사람들이 왜 당신을 싫어하는지 알 것 같아요.";
            choice_02.text = "소크라테스 씨는 아는 게 많군요.";
        }

        else if (talkIndex == 32 || talkIndex == 40)
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

    public void Branch_Plato()
    {
        if (talkIndex == 6)
        {
            choice.SetActive(true);
            choice_01.text = "긍정한다";
            choice_02.text = "부정한다";
        }

        else if (talkIndex == 18)
        {
            choice.SetActive(true);
            choice_01.text = "아주 좋은데요?";
            choice_02.text = "말도 안 되는데요?";
        }

        else if (talkIndex == 24)
        {
            choice.SetActive(true);
            choice_01.text = "그 힘으로 복수를 해보는 건?";
            choice_02.text = "울지 마요. 나의 ㅇr7ㅣ사슴…☆";
        }
    }

    public void Branch_Pytha()
    {
        choice.SetActive(true);
        choice_01.text = "미안해요 피타고라스 씨.";
        choice_02.text = "그러게 왜 내 앞길을 막아요?";
    }

    public void Branch_Archi()
    {
        if (talkIndex == 10)
        {
            choice.SetActive(true);
            choice_01.text = "왜 여기서 이러고 있는 거예요!?!??";
            choice_02.text = "아무 말 없이 가만히 있는다";
        }

        else if (talkIndex == 33)
        {
            choice.SetActive(true);
            choice_01.text = "진정해요!";
            choice_02.text = "다른 이야기로 화제를 돌린다.";
        }
    }

    public void Branch_Epicuru()
    {
        choice.SetActive(true);
        choice_01.text = "고… 고마워요.";
        choice_02.text = "소문과는 거리가 먼 공간이네요.";
    }


    public void Choice_Up()
    {
        if (enemyId == 1001)
        {
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

            else if (talkIndex == 32 || talkIndex == 40)
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

        if (enemyId == 1002)
        {
            if (talkIndex == 6)
            {
                talkIndex = 7;
                Talk(enemyId);
            }

            else if (talkIndex == 18)
            {
                talkIndex = 19;
                Talk(enemyId);
            }

            else if (talkIndex == 24)
            {
                talkIndex = 25;
                Talk(enemyId);
            }
        }

        if (enemyId == 1004)
        {
            talkIndex = 15;
            Talk(enemyId);
        }

        if (enemyId == 1005)
        {
            if (talkIndex == 10)
            {
                talkIndex = 11;
                Talk(enemyId);
            }

            else if (talkIndex == 33)
            {
                talkIndex = 34;
                Talk(enemyId);
            }
        }

        if (enemyId == 1007)
        {
            talkIndex = 17;
            Talk(enemyId);
        }

        choice.SetActive(false);
    }

    public void Choice_Down()
    {
        if (enemyId == 1001)
        {
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

            else if (talkIndex == 32 || talkIndex == 40)
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

        if (enemyId == 1002)
        {
            if (talkIndex == 6)
            {
                talkIndex = 11;
                Talk(enemyId);
            }

            else if (talkIndex == 18)
            {
                talkIndex = 33;
                Talk(enemyId);
            }

            else if (talkIndex == 24)
            {
                talkIndex = 29;
                Talk(enemyId);
            }
        }

        if (enemyId == 1004)
        {
            talkIndex = 17;
            Talk(enemyId);
        }

        if (enemyId == 1005)
        {
            if (talkIndex == 10)
            {
                talkIndex = 13;
                Talk(enemyId);
            }

            else if (talkIndex == 33)
            {
                talkIndex = 40;
                Talk(enemyId);
            }
        }

        if (enemyId == 1007)
        {
            talkIndex = 21;
            Talk(enemyId);
        }

        choice.SetActive(false);
    }

    IEnumerator CameraShake()
    {
        camera.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        camera.SetActive(false);
    }

    //도감에서 시뮬레이션 들어가기
    IEnumerator SimulOn()
    {
        simulFade.SetActive(true);
        bookFade.SetActive(true);
        titleEffect.SetActive(false);

        yield return new WaitForSeconds(4.0f);

        otherCanvas[0].SetActive(false);
        otherCanvas[1].SetActive(false);
        simulCanvas.SetActive(true);
        logo.SetActive(false);

        if (enemyId == 1001)
            bg.sprite = bgSource[1];

        else if (enemyId == 1002)
            bg.sprite = bgSource[3];

        else if (enemyId == 1003)
            bg.sprite = bgSource[4];

        else if (enemyId == 1004)
            bg.sprite = bgSource[5];

        else if (enemyId == 1005)
            bg.sprite = bgSource[1];

        else if (enemyId == 1006)
            bg.sprite = bgSource[3];

        else if (enemyId == 1007)
            bg.sprite = bgSource[3];

        else if (enemyId == 1008)
            bg.sprite = bgSource[2];

        StartCoroutine(SimulFadeInCorutine());

        yield return new WaitForSeconds(2.0f);

        simulFade.SetActive(false);
        bookFade.SetActive(false);
        talkSet.SetActive(true);

        talk.SetMsg(SimulDataManager.GetTalk(1001, 0));
        talkIndex = 1;
    }

    //시뮬레이션에서 도감으로 넘어가기
    IEnumerator BookOn()
    {
        simulFade.SetActive(true);
        bookFade.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        //상태 초기화 (이전 미연시장면에서 사용됐던 것들 초기화)
        talkSet.SetActive(false);
        portraitImg.SetActive(false);
        extraImg_01.SetActive(false);
        extraImg_02.SetActive(false);
        extraImg_03.SetActive(false);
        SimulDataManager.GetName(1).SetActive(false);
        SimulDataManager.GetName(2).SetActive(false);
        SimulDataManager.GetName(3).SetActive(false);
        SimulDataManager.GetName(4).SetActive(false);
        SimulDataManager.GetName(5).SetActive(false);
        Thales_Fade.SetActive(false);
        enemyId = 0;
        talkIndex = 0;

        //도감창 활성화
        otherCanvas[0].SetActive(true);
        otherCanvas[1].SetActive(true);
        simulCanvas.SetActive(false);
        logo.SetActive(true);
        bg.sprite = bgSource[0];

        StartCoroutine(BookFadeInCorutine());

        yield return new WaitForSeconds(2.0f);

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

    IEnumerator ThalesFadeOutCorutine()
    {
        float count = 0;

        while (count <= 1.0f)
        {
            count += 0.01f;
            yield return new WaitForSeconds(0.02f);
            Thales_Fade.GetComponent<Image>().color = new Color(0, 0, 0, count);
        }
    }

    IEnumerator ChangeBG_Pytha()
    {
        SimulDataManager.GetName(1).SetActive(true);
        SimulDataManager.GetName(2).SetActive(false);
        SimulDataManager.GetName(3).SetActive(false);
        SimulDataManager.GetName(4).SetActive(false);
        SimulDataManager.GetName(5).SetActive(false);

        yield return new WaitForSeconds(1.0f);

        bg.sprite = bgSource[2];

        yield return new WaitForSeconds(2.0f);

        portraitImg.SetActive(true);

        SimulDataManager.GetName(1).SetActive(false);
        SimulDataManager.GetName(2).SetActive(true);

        talk.SetMsg(SimulDataManager.GetTalk(enemyId, talkIndex).Split(':')[0]);
        talkIndex++;
    }

    IEnumerator ChangeBG_Thales()
    {
        simulFade.SetActive(true);

        float count = 0;

        while (count <= 1.0f)
        {
            count += 0.01f;
            yield return new WaitForSeconds(0.02f);
            simulFade.GetComponent<Image>().color = new Color(255, 255, 255, count);
        }

        yield return new WaitForSeconds(1f);

        if (enemyId == 1006)
        {
            if (talkIndex == 5)
            {
                bg.sprite = bgSource[1];
                talk.SetMsg(SimulDataManager.GetTalk(enemyId, talkIndex).Split(':')[0]);
                talkIndex++;
            }

            else if (talkIndex == 36)
            {
                bg.sprite = bgSource[3];
                talk.SetMsg(SimulDataManager.GetTalk(enemyId, talkIndex).Split(':')[0]);
                talkIndex++;
            }
        }

        while (count >= 0)
        {
            count -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            simulFade.GetComponent<Image>().color = new Color(255, 255, 255, count);
        }

        simulFade.SetActive(false);
    }
}
