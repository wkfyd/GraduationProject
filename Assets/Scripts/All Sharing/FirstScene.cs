using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public void StartClick()
    {
        SceneManager.LoadScene("GameTitle");
        SaveData.GameLoad();
    }
}
