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
    public GameObject extraImg_01;
    public GameObject extraImg_02;
    public GameObject extraImg_03;
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
    public GameObject titleEffect;

    public void Action()
    {
        Talk(enemyId);
    }

    void Talk(int enemyId)
    {
        string talkData = SimulDataManager.GetTalk(enemyId, talkIndex);

        //Ÿ����ȿ�����̸� return���� ����
        if (talk.isAnim)
        {
            talk.SetMsg(""); //�� ���� �־ ������ SetMsg�� �����ؼ� Ÿ���οϷḦ ���Ѿ��ϱ� ������
            return;
        }

        //����
        if (talkData == null)
        {
            StartCoroutine(BookOn());
            StartCoroutine(SimulFadeOutCorutine());
            return;
        }

        //ȭ�� ����
        talkingChar_Next = int.Parse(talkData.Split(':')[1]);

        if ((talkingChar == 0 || talkingChar == 1) && talkingChar_Next == 2 ||
             talkingChar == 2 && (talkingChar_Next == 1 || talkingChar_Next == 0))
        {
            talkPanel.SetTrigger("Talk Up And Down");
            talkingChar = talkingChar_Next;
        }

        //��ũ���׽�
        if (enemyId == 1001)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 4)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 52 || talkIndex == 60)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //���� �б�
            else if (talkIndex == 8 || talkIndex == 16 || talkIndex == 32 || talkIndex == 40 || talkIndex == 46)
            {
                Branch_Socra();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //���亯�� ���ýÿ� �Ʒ��䵵 ���� �����⶧���� �ε��� ����
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

            //ī�޶� ��鸲
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

        //�ö���
        if (enemyId == 1002)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 1)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 50)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //���� �б�
            else if (talkIndex == 6 || talkIndex == 18 || talkIndex == 24)
            {
                Branch_Plato();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //���亯�� ���ýÿ� �Ʒ��䵵 ���� �����⶧���� �ε��� ����
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
        }

        //�Ƹ������ڷ���
        if (enemyId == 1003)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 1 || talkIndex == 11 || talkIndex == 18)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 8 || talkIndex == 16 || talkIndex == 33)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //ī�޶� ��鸲
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

        //��Ÿ���
        if (enemyId == 1004)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 4 || talkIndex == 43 || talkIndex == 47)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 33 || talkIndex == 45)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //����Ʈ�� ����
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

            //����Ʈ�� ����
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

            //���� �б�
            else if (talkIndex == 14)
            {
                Branch_Pytha();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //���亯�� ���ýÿ� �Ʒ��䵵 ���� �����⶧���� �ε��� ����
            else if (talkIndex == 16)
            {
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex = 20;
            }

            //ī�޶� ��鸲
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

        //�Ƹ�Ű�޵���
        if (enemyId == 1005)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 8 || talkIndex == 51)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 38)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //���� �б�
            else if (talkIndex == 10 || talkIndex == 33)
            {
                Branch_Archi();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //���亯�� ���ýÿ� �Ʒ��䵵 ���� �����⶧���� �ε��� ����
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

            //ī�޶� ��鸲
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

        //Ż����
        if (enemyId == 1006)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 6 || talkIndex == 51)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 35)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //ī�޶� ��鸲
            else if (talkIndex == 9 || talkIndex == 15 || talkIndex == 19 || talkIndex == 24 || talkIndex == 38)
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

        //������ν�
        if (enemyId == 1007)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 12)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 34)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //���� �б�
            else if (talkIndex == 16)
            {
                Branch_Epicuru();
                talk.SetMsg(talkData.Split(':')[0]);
            }

            //���亯�� ���ýÿ� �Ʒ��䵵 ���� �����⶧���� �ε��� ����
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

        //ŰƼ���� ����
        if (enemyId == 1008)
        {
            //�Ϸ���Ʈ ����
            if (talkIndex == 12 || talkIndex == 30)
            {
                portraitImg.SetActive(true);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //�Ϸ���Ʈ ����
            else if (talkIndex == 16 || talkIndex == 33)
            {
                portraitImg.SetActive(false);
                talk.SetMsg(talkData.Split(':')[0]);
                talkIndex++;
            }

            //ī�޶� ��鸲
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


        //�̸��ڽ�
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
    }

    public void TalkButton_Socra()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1001;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "��ũ���׽�";
    }

    public void TalkButton_Plato()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1002;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "�ö���";
    }

    public void TalkButton_Aristo()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1003;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "�Ƹ������ڷ���";
    }

    public void TalkButton_Pytha()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1004;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "��Ÿ���";
    }

    public void TalkButton_Archi()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1005;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "�Ƹ�Ű�޵���";
    }

    public void TalkButton_Thales()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1006;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "Ż����";
    }

    public void TalkButton_Epicuru()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1007;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "������ν�";
    }

    public void TalkButton_Zeno()
    {
        bookFade.SetActive(true);

        StartCoroutine(SimulOn());
        StartCoroutine(BookFadeOutCorutine());

        enemyId = 1008;

        portraitImg = SimulDataManager.GetPortrait(enemyId);
        enemyName.text = "����";
    }

    public void Branch_Socra()
    {
        if (talkIndex == 8)
        {
            choice.SetActive(true);
            choice_01.text = "�翬����?";
            choice_02.text = "�ƴ���?";
        }

        else if (talkIndex == 16)
        {
            choice.SetActive(true);
            choice_01.text = "������� �� ����� �Ⱦ��ϴ��� �� �� ���ƿ�.";
            choice_02.text = "��ũ���׽� ���� �ƴ� �� ������.";
        }

        else if (talkIndex == 32 || talkIndex == 40)
        {
            choice.SetActive(true);
            choice_01.text = "�����Ѵ�";
            choice_02.text = "�������� �ʴ´�";
        }

        else if (talkIndex == 46)
        {
            choice.SetActive(true);
            choice_01.text = "�����Ѵ�";
            choice_02.text = "�����Ѵ�";
        }
    }

    public void Branch_Plato()
    {
        if (talkIndex == 6)
        {
            choice.SetActive(true);
            choice_01.text = "�����Ѵ�";
            choice_02.text = "�����Ѵ�";
        }

        else if (talkIndex == 18)
        {
            choice.SetActive(true);
            choice_01.text = "���� ��������?";
            choice_02.text = "���� �� �Ǵµ���?";
        }

        else if (talkIndex == 24)
        {
            choice.SetActive(true);
            choice_01.text = "�� ������ ������ �غ��� ��?";
            choice_02.text = "���� ����. ���� ��r7�ӻ罿����";
        }
    }

    public void Branch_Pytha()
    {
        choice.SetActive(true);
        choice_01.text = "�̾��ؿ� ��Ÿ��� ��.";
        choice_02.text = "�׷��� �� �� �ձ��� ���ƿ�?";
    }

    public void Branch_Archi()
    {
        if (talkIndex == 10)
        {
            choice.SetActive(true);
            choice_01.text = "�� ���⼭ �̷��� �ִ� �ſ���!?!??";
            choice_02.text = "�ƹ� �� ���� ������ �ִ´�";
        }

        else if (talkIndex == 33)
        {
            choice.SetActive(true);
            choice_01.text = "�����ؿ�!";
            choice_02.text = "�ٸ� �̾߱�� ȭ���� ������.";
        }
    }

    public void Branch_Epicuru()
    {
        choice.SetActive(true);
        choice_01.text = "�� ������.";
        choice_02.text = "�ҹ����� �Ÿ��� �� �����̳׿�.";
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
        talkIndex = 0;

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
