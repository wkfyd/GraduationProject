using UnityEngine;

public class CameraShake : MonoBehaviour
{
    GameObject obj;

    public float shakeIntensity; // ������ ����
    public float shakeDuration = 1f; // ������ ���� �ð�

    private Vector3 initialPosition; // �ʱ� ��ġ
    private float elapsedTime = 0f; // ��� �ð�

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

            // ������ ȿ���� ���� �������� ���� �����Ͽ� ī�޶� ��ġ�� ����
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            obj.transform.position = initialPosition + shakeOffset;
            Debug.Log(1);
        }
    }
}
