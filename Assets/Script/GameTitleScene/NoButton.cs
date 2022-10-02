using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    private GameObject startPopUp;
    public void NoButtonClick()
    {
        startPopUp = GameObject.Find("Canvas").transform.Find("StartButton_PopUp").gameObject;
        startPopUp.SetActive(false);
    }
}
