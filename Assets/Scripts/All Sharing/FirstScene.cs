using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("GameTitle");
    }
}
