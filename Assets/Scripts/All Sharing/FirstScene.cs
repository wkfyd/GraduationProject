using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public void Start()
    {
        SceneManager.LoadScene("GameTitle");
        SaveData.GameLoad();
    }
}
