using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBGM : MonoBehaviour
{
    public AudioSource bgm;

    private void Awake()
    {
        GameObject previousBgm = GameObject.Find("BGM"); // BGM을 재생하는 GameObject의 이름으로 변경해야 합니다.
        if (previousBgm != null)
        {
            Destroy(previousBgm);
        }
    }

    void Start()
    {
        bgm.Play();
    }
}
