using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
    private GameObject Stage01_PopUp;
    public void OnClicked()
    {
        Stage01_PopUp = GameObject.Find("Canvas").transform.Find("Stage01_PopUp").gameObject;
        Stage01_PopUp.SetActive(true);
    }
}
