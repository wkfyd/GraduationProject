using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject Tuto_Text;

    public int enemyId;

    private GameObject Stage;

    //public SaveData saveData;

    System.Random random = new System.Random();

    void Start()
    {
        //saveData = GameObject.Find("SaveData").GetComponent<SaveData>();

        //if(saveData.Tutorial == 0)
        StartTutorial();
        EnemyRandom_Stg1();
        Debug.Log(enemyId);
    }

    void StartTutorial()
    {
        Tutorial.SetActive(true);
        Invoke("StartTutoText", 1f);
    }

    void StartTutoText()
    {
        Tuto_Text.SetActive(true);
    }

    public int EnemyRandom_Stg1()
    {
        return enemyId = random.Next(1013, 1018);
    }

    public void Stage01()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage01_PopUp").gameObject;
        Stage.SetActive(true);
    }

    public void Stage02()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage02_PopUp").gameObject;
        Stage.SetActive(true);
    }

    public void Stage03()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage03_PopUp").gameObject;
        Stage.SetActive(true);
    }

    public void Stage04()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage04_PopUp").gameObject;
        Stage.SetActive(true);
    }

    public void Stage05()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage05_PopUp").gameObject;
        Stage.SetActive(true);
    }

    public void Stage06()
    {
        Stage = GameObject.Find("Canvas").transform.Find("Stage06_PopUp").gameObject;
        Stage.SetActive(true);
    }
    public void FreeMode()
    {
        Stage = GameObject.Find("Canvas").transform.Find("FreeMode").gameObject;
        Stage.SetActive(true);
    }
}
