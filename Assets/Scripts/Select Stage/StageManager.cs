using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject pause;

    public GameObject Tutorial;
    public GameObject Tuto_Text;
    public Animator TutoAnim;

    public RectTransform arrow;
    public GameObject stage_PopUp;
    public TextMeshProUGUI round_Text;

    public int enemyId;     //적 id
    public int stageNum;    //현재 스테이지

    [Header("StageIcon")]
    public Button stg01;
    public TextMeshProUGUI stg01_text;
    public Button stg02;
    public TextMeshProUGUI stg02_text;
    public Button stg03;
    public TextMeshProUGUI stg03_text;
    public Button stg04;
    public TextMeshProUGUI stg04_text;
    public Button stg05;
    public TextMeshProUGUI stg05_text;
    public Button stg06;
    public TextMeshProUGUI stg06_text;

    [Header("Enemy List")]
    public GameObject[] enemyArr;

    private GameObject Stage_PopUp;

    System.Random random = new System.Random();
    void Start()
    {
        //튜토리얼 미진행시
        if (SaveData.isTutorial == 0 && SaveData.currentEnemy_Id == 0)
        {
            StartTutorial();

            stageNum = 1;                 //1스테이지에
            round_Text.text = "1라운드";
            enemyId = EnemyRandom_Stg1(); //적 랜덤

            SaveData.currentStage = stageNum;
            SaveData.currentEnemy_Id = enemyId;

            stg01.interactable = true;
            stg02.interactable = false;
            stg02_text.color = new Color(255, 255, 255);
            stg03.interactable = false;
            stg03_text.color = new Color(255, 255, 255);
            stg04.interactable = false;
            stg04_text.color = new Color(255, 255, 255);
            stg05.interactable = false;
            stg05_text.color = new Color(255, 255, 255);
            stg06.interactable = false;
            stg06_text.color = new Color(255, 255, 255);

            arrow.anchoredPosition = new Vector3(-577f, 320f, 0);

            Stage01_PopUp_Enemy();
        }

        else
        {
            //튜토리얼 아닐시
            if (SaveData.currentStage == 1)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg1();
                }

                round_Text.text = "1라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = true;
                stg02.interactable = false;
                stg02_text.color = new Color(255, 255, 255);
                stg03.interactable = false;
                stg03_text.color = new Color(255, 255, 255);
                stg04.interactable = false;
                stg04_text.color = new Color(255, 255, 255);
                stg05.interactable = false;
                stg05_text.color = new Color(255, 255, 255);
                stg06.interactable = false;
                stg06_text.color = new Color(255, 255, 255);

                arrow.anchoredPosition = new Vector3(-577f, 320f, 0);

                Stage01_PopUp_Enemy();
            }

            else if (SaveData.currentStage == 2)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg2or3();
                }

                round_Text.text = "2라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = false;
                stg01_text.color = new Color(255, 255, 255);
                stg02.interactable = true;
                stg03.interactable = false;
                stg03_text.color = new Color(255, 255, 255);
                stg04.interactable = false;
                stg04_text.color = new Color(255, 255, 255);
                stg05.interactable = false;
                stg05_text.color = new Color(255, 255, 255);
                stg06.interactable = false;
                stg06_text.color = new Color(255, 255, 255);

                arrow.anchoredPosition = new Vector3(-368f, 405f, 0);

                Stage02or3_PopUp_Enemy();
            }

            else if (SaveData.currentStage == 3)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg2or3();
                }

                round_Text.text = "3라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = false;
                stg01_text.color = new Color(255, 255, 255);
                stg02.interactable = false;
                stg02_text.color = new Color(255, 255, 255);
                stg03.interactable = true;
                stg04.interactable = false;
                stg04_text.color = new Color(255, 255, 255);
                stg05.interactable = false;
                stg05_text.color = new Color(255, 255, 255);
                stg06.interactable = false;
                stg06_text.color = new Color(255, 255, 255);

                arrow.anchoredPosition = new Vector3(-142f, 424f, 0);

                Stage02or3_PopUp_Enemy();
            }

            else if (SaveData.currentStage == 4)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg4();
                }

                round_Text.text = "4라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = false;
                stg01_text.color = new Color(255, 255, 255);
                stg02.interactable = false;
                stg02_text.color = new Color(255, 255, 255);
                stg03.interactable = false;
                stg03_text.color = new Color(255, 255, 255);
                stg04.interactable = true;
                stg05.interactable = false;
                stg05_text.color = new Color(255, 255, 255);
                stg06.interactable = false;
                stg06_text.color = new Color(255, 255, 255);

                arrow.anchoredPosition = new Vector3(88f, 325f, 0);

                Stage04_PopUp_Enemy();
            }

            else if (SaveData.currentStage == 5)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg5();
                }

                round_Text.text = "5라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = false;
                stg01_text.color = new Color(255, 255, 255);
                stg02.interactable = false;
                stg02_text.color = new Color(255, 255, 255);
                stg03.interactable = false;
                stg03_text.color = new Color(255, 255, 255);
                stg04.interactable = false;
                stg04_text.color = new Color(255, 255, 255);
                stg05.interactable = true;
                stg06.interactable = false;
                stg06_text.color = new Color(255, 255, 255);

                arrow.anchoredPosition = new Vector3(308f, 376f, 0);

                Stage05_PopUp_Enemy();
            }

            else if (SaveData.currentStage == 6)
            {
                if (SaveData.currentEnemy_Id == 0)
                {
                    SaveData.currentEnemy_Id = EnemyRandom_Stg6();
                }

                round_Text.text = "6라운드";

                stageNum = SaveData.currentStage;
                enemyId = SaveData.currentEnemy_Id;

                stg01.interactable = false;
                stg01_text.color = new Color(255, 255, 255);
                stg02.interactable = false;
                stg02_text.color = new Color(255, 255, 255);
                stg03.interactable = false;
                stg03_text.color = new Color(255, 255, 255);
                stg04.interactable = false;
                stg04_text.color = new Color(255, 255, 255);
                stg05.interactable = false;
                stg05_text.color = new Color(255, 255, 255);
                stg06.interactable = true;

                arrow.anchoredPosition = new Vector3(548f, 280f, 0);

                Stage06_PopUp_Enemy();
            }
        }

        SaveData.GameSave();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pause.SetActive(true);
    }
    void Stage01_PopUp_Enemy()
    {
        if (SaveData.currentEnemy_Id == 1013)
        {
            for (int i = 12; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[12].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1014)
        {
            for (int i = 12; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[13].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1015)
        {
            for (int i = 12; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[14].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1016)
        {
            for (int i = 12; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[15].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1017)
        {
            for (int i = 12; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[16].SetActive(true);
        }
    }

    void Stage02or3_PopUp_Enemy()
    {
        if (SaveData.currentEnemy_Id == 1009)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[8].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1010)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[9].SetActive(true);
        }
        else if (SaveData.currentEnemy_Id == 1011)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[10].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1012)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[11].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1013)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[12].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1014)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[13].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1015)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[14].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1016)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[15].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1017)
        {
            for (int i = 8; i < 17; i++)
                enemyArr[i].SetActive(false);

            enemyArr[16].SetActive(true);
        }
    }

    void Stage04_PopUp_Enemy()
    {
        if (SaveData.currentEnemy_Id == 1005)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[4].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1006)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[5].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1007)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[6].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1008)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[7].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1009)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[8].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1010)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[9].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1011)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[10].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1012)
        {
            for (int i = 4; i < 12; i++)
                enemyArr[i].SetActive(false);

            enemyArr[11].SetActive(true);
        }
    }

    void Stage05_PopUp_Enemy()
    {
        if (SaveData.currentEnemy_Id == 1005)
        {
            for (int i = 4; i < 8; i++)
                enemyArr[i].SetActive(false);

            enemyArr[4].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1006)
        {
            for (int i = 4; i < 8; i++)
                enemyArr[i].SetActive(false);

            enemyArr[5].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1007)
        {
            for (int i = 4; i < 8; i++)
                enemyArr[i].SetActive(false);

            enemyArr[6].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1008)
        {
            for (int i = 4; i < 8; i++)
                enemyArr[i].SetActive(false);

            enemyArr[7].SetActive(true);
        }
    }

    void Stage06_PopUp_Enemy()
    {
        if (SaveData.currentEnemy_Id == 1001)
        {
            for (int i = 0; i < 4; i++)
                enemyArr[i].SetActive(false);

            enemyArr[0].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1002)
        {
            for (int i = 0; i < 4; i++)
                enemyArr[i].SetActive(false);

            enemyArr[1].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1003)
        {
            for (int i = 0; i < 4; i++)
                enemyArr[i].SetActive(false);

            enemyArr[2].SetActive(true);
        }

        else if (SaveData.currentEnemy_Id == 1004)
        {
            for (int i = 0; i < 4; i++)
                enemyArr[i].SetActive(false);

            enemyArr[3].SetActive(true);
        }
    }



    public int EnemyRandom_Stg1()
    {
        int result = random.Next(1013, 1018);

        if (SaveData.isHippa == 1 && SaveData.isEucli == 1 && SaveData.isStoicism == 1 &&
            SaveData.isEpicuri == 1 && SaveData.isSophist == 1)
        {
            return result;
        }

        if (result == 1013)
        {
            if (SaveData.isHippa != 1)
                return result;
        }

        else if (result == 1014)
        {
            if (SaveData.isEucli != 1)
                return result;
        }

        else if (result == 1015)
        {
            if (SaveData.isStoicism != 1)
                return result;
        }

        else if (result == 1016)
        {
            if (SaveData.isEpicuri != 1)
                return result;
        }

        else if (result == 1017)
        {
            if (SaveData.isSophist != 1)
                return result;
        }

        return EnemyRandom_Stg1();
    }

    public int EnemyRandom_Stg2or3()
    {
        int[] enemyIdRandom = new int[] { 3, 3, 3, 4, 4, 4, 4, 4, 4, 4 };

        int tmp = random.Next(0, 10);

        int enemyRare = enemyIdRandom[tmp];

        int result = 0;

        if (enemyRare == 3)
            result = random.Next(1009, 1013);
        else
            result = random.Next(1013, 1018);

        if (SaveData.isDiog == 1 && SaveData.isProta == 1 && SaveData.isThrasy == 1 && SaveData.isGorgi == 1 &&
            SaveData.isHippa == 1 && SaveData.isEucli == 1 && SaveData.isStoicism == 1 &&
            SaveData.isEpicuri == 1 && SaveData.isSophist == 1)
        {
            return result;
        }

        if (result == 1009)
        {
            if (SaveData.isDiog != 1)
                return result;
        }

        else if (result == 1010)
        {
            if (SaveData.isProta != 1)
                return result;
        }

        else if (result == 1011)
        {
            if (SaveData.isThrasy != 1)
                return result;
        }

        else if (result == 1012)
        {
            if (SaveData.isGorgi != 1)
                return result;
        }

        else if (result == 1013)
        {
            if (SaveData.isHippa != 1)
                return result;
        }

        else if (result == 1014)
        {
            if (SaveData.isEucli != 1)
                return result;
        }

        else if (result == 1015)
        {
            if (SaveData.isStoicism != 1)
                return result;
        }

        else if (result == 1016)
        {
            if (SaveData.isEpicuri != 1)
                return result;
        }

        else if (result == 1017)
        {
            if (SaveData.isSophist != 1)
                return result;
        }

        return EnemyRandom_Stg2or3();
    }

    public int EnemyRandom_Stg4()
    {
        int[] enemyIdRandom = new int[] { 2, 3, 3, 3, 3, 3, 3, 3, 3, 3 };

        int tmp = random.Next(0, 10);

        int enemyRare = enemyIdRandom[tmp];

        int result = 0;

        if (enemyRare == 2)
            result = random.Next(1005, 1009);
        else
            result = random.Next(1009, 1013);

        if (SaveData.isArchi == 1 && SaveData.isThales == 1 && SaveData.isEpicuru == 1 && SaveData.isZeno == 1 &&
            SaveData.isDiog == 1 && SaveData.isProta == 1 && SaveData.isThrasy == 1 && SaveData.isGorgi == 1)
        {
            return result;
        }

        if (result == 1005)
        {
            if (SaveData.isArchi != 1)
                return result;
        }

        else if (result == 1006)
        {
            if (SaveData.isThales != 1)
                return result;
        }

        else if (result == 1007)
        {
            if (SaveData.isEpicuru != 1)
                return result;
        }

        else if (result == 1008)
        {
            if (SaveData.isZeno != 1)
                return result;
        }

        else if (result == 1009)
        {
            if (SaveData.isDiog != 1)
                return result;
        }

        else if (result == 1010)
        {
            if (SaveData.isProta != 1)
                return result;
        }

        else if (result == 1011)
        {
            if (SaveData.isThrasy != 1)
                return result;
        }

        else if (result == 1012)
        {
            if (SaveData.isGorgi != 1)
                return result;
        }

        return EnemyRandom_Stg4();
    }

    public int EnemyRandom_Stg5()
    {
        int result = random.Next(1005, 1009);

        if (SaveData.isArchi == 1 && SaveData.isThales == 1 && SaveData.isEpicuru == 1 && SaveData.isZeno == 1)
            return result;

        if (result == 1005)
        {
            if (SaveData.isArchi != 1)
                return result;
        }

        else if (result == 1006)
        {
            if (SaveData.isThales != 1)
                return result;
        }

        else if (result == 1007)
        {
            if (SaveData.isEpicuru != 1)
                return result;
        }

        else if (result == 1008)
        {
            if (SaveData.isZeno != 1)
                return result;
        }

        return EnemyRandom_Stg5();
    }

    public int EnemyRandom_Stg6()
    {
        int result = random.Next(1001, 1005);

        if (SaveData.isSocra == 1 && SaveData.isPlato == 1 && SaveData.isAristo == 1 && SaveData.isPytha == 1)
        {
            return result;
        }

        if (result == 1001)
        {
            if (SaveData.isSocra != 1)
                return result;
        }

        else if (result == 1002)
        {
            if (SaveData.isPlato != 1)
                return result;
        }

        else if (result == 1003)
        {
            if (SaveData.isAristo != 1)
                return result;
        }

        else if (result == 1004)
        {
            if (SaveData.isPytha != 1)
                return result;
        }

        return EnemyRandom_Stg6();
    }

    public void Stage01()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage01_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }

    public void Stage02()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage02_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }

    public void Stage03()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage03_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }

    public void Stage04()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage04_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }

    public void Stage05()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage05_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }

    public void Stage06()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("Stage06_PopUp").gameObject;
        Stage_PopUp.SetActive(true);
    }
    public void FreeMode()
    {
        Stage_PopUp = GameObject.Find("Canvas").transform.Find("FreeMode").gameObject;
        Stage_PopUp.SetActive(true);
    }

    void StartTutorial()
    {
        Tutorial.SetActive(true);
        Invoke("StartTutoText", 1f);
    }

    void StartTutoText()
    {
        Tuto_Text.SetActive(true);
        TutoAnim.SetTrigger("Talk Show");
    }
}
