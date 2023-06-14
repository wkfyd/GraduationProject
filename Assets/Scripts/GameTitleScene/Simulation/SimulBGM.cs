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

    IEnumerator PlayBGM_After()
    {
        yield return new WaitForSeconds(bgmA.length);

        bgmSource.loop = true;
        bgmSource.clip = bgmB;
        bgmSource.Play();
    }

    void OnEnable()
    {
        bgmSource = GameObject.Find("BGM").GetComponent<AudioSource>();

        // ������Ʈ�� Ȱ��ȭ�� �� BGM�� ����
        bgmSource.Stop();
        bgmSource.loop = false;
        bgmSource.clip = bgmA;
        
        bgmSource.PlayOneShot(bgmA);

        // BGM�� �� ���� ����ϰ� ����Ǹ� �ٸ� BGM�� ���� ���
        StartCoroutine(PlayBGM_After());

    }

    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ�� �� BGM �������
        if (!isBGM_DPlaying)
        {
            bgmSource.Stop();
            bgmSource.loop = true;
            bgmSource.clip = bgmC;
            bgmSource.Play();
        }
    }
}

