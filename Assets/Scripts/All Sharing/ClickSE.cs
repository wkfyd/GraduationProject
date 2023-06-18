using UnityEngine;

public class ClickSE : MonoBehaviour
{
    public AudioClip soundA;
    public AudioClip soundB;
    public AudioClip buzzer;

    //SE 볼륨 수치
    private float volumeA = 0.3f;
    private float volumeB = 0.2f;
    private float volumeBz = 0.2f;


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

        int condition = 0;

        if (hit.collider != null && hit.collider.CompareTag("Block"))
            condition = 0;
        else if (hit.collider != null && hit.collider.CompareTag("ActiveFalse"))
            condition = 1;
        else
            condition = 2;

        switch (condition)
        {
            case 0:
                PlaySound(soundB, volumeB);
                break;

            case 1:
                PlaySound(buzzer, volumeBz);
                break;

            case 2:
                PlaySound(soundA, volumeA);
                break;
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
