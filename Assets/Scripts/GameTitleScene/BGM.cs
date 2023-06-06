using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    // BGM ����� �ҽ�
    public AudioSource bgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM ���
    public void PlayBGM(AudioClip bgmClip)
    {
        bgm.clip = bgmClip;
        bgm.Play();
    }

}
