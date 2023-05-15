using UnityEngine;

public class CameraShake : MonoBehaviour
{
    GameObject obj;

    public float shakeIntensity; // 뒤흔들기 강도
    public float shakeDuration = 1f; // 뒤흔들기 지속 시간

    private Vector3 initialPosition; // 초기 위치
    private float elapsedTime = 0f; // 경과 시간

    void Awake()
    {
        obj = GameObject.Find("Main Camera");
        initialPosition = obj.transform.position;
    }

    void Update()
    {
        if (elapsedTime < shakeDuration)
        {
            
            elapsedTime += Time.deltaTime;

            // 뒤흔들기 효과를 위해 무작위로 값을 생성하여 카메라 위치를 변경
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            obj.transform.position = initialPosition + shakeOffset;
            Debug.Log(1);
        }
    }
}
