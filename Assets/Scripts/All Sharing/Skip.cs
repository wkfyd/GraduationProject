using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    public void SkipButtonDown()  //On Click() ÇÔ¼ö
    {
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            SceneManager.LoadScene("Select Stage");
        }

        if (SceneManager.GetActiveScene().name == "Ending")
        {
            SceneManager.LoadScene("EndingCredit");
        }
    }
}
