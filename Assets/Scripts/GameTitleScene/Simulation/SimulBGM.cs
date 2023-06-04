using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulBGM : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioClip bgmA;  //intro BGM
    public AudioClip bgmB;  //loop BGM
    public AudioClip bgmC;  //title BGM
    public float volumeA = 0.5f;

    private bool isBGM_DPlaying = false;

    void Start()
    {
        // BGM을 한 번만 재생하고 종료되면 다른 BGM을 루프 재생
        StartCoroutine(PlayBGM_After());
    }

    IEnumerator PlayBGM_After()
    {
        yield return new WaitForSeconds(bgmA.length);

        bgmSource.loop = true;
        bgmSource.clip = bgmB;
        bgmSource.Play();
    }

    void OnEnable()
    {
        // 오브젝트가 활성화될 때 BGM을 변경
        bgmSource.Stop();
        bgmSource.loop = false;
        bgmSource.clip = bgmA;
        
        bgmSource.PlayOneShot(bgmA);
        
    }

    void OnDisable()
    {
        // 오브젝트가 비활성화될 때 BGM 원래대로
        if (!isBGM_DPlaying)
        {
            bgmSource.Stop();
            bgmSource.loop = true;
            bgmSource.clip = bgmC;
            bgmSource.Play();
        }
    }
}

