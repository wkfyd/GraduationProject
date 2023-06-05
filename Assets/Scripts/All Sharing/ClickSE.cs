using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSE : MonoBehaviour
{
    public AudioClip soundA;
    public AudioClip soundB;

    //SE 볼륨 수치
    private float volumeA = 0.4f;
    private float volumeB = 0.2f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AorB();
        }
    }

    private void AorB()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        bool condition = hit.collider != null && hit.collider.CompareTag("Block");

        if (condition)
        {
            PlaySound(soundB, volumeB);
        }
        else
        {
            PlaySound(soundA, volumeA);
        }

    }

    private void PlaySound(AudioClip soundClip, float volume)
    {
        Debug.Log("Sound played: " + soundClip);


        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = soundClip;
        audioSource.Play();

        // 사운드 재생이 끝나면 게임 오브젝트 파괴
        Destroy(soundObject, soundClip.length);
    }

}
