using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public AudioClip startClip;

    void OnDisable()
    {
        PlaySound(startClip, 0.5f);
    }

    public void PlaySound(AudioClip soundClip, float volume)
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
