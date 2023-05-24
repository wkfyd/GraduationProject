using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Tutorial;

    private GameObject Stage;

    void Start()
    {
        Invoke("StartTutorial", 1f);
    }

    void StartTutorial()
    {
        Tutorial.SetActive(true);
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
