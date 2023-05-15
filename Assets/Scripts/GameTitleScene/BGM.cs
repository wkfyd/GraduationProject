using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;

    private void Awake()
    {
        BGM[] bgm = FindObjectsOfType<BGM>();
        if (bgm.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bgm.Play();
    }
}
