using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScaleScript : MonoBehaviour
{
    [Header("scale speed")]  //�ν�����â ����
    [Range(0.1f, 10f)] public float size = 1f;   //������Ʈ ũ��
    [Range(0.1f, 10f)] public float scaleSpeed = 1f; //ũ�⺯�� �ӵ�

    public Button stage_Btn;

    float timer = 0;

    void Awake()
    {
        stage_Btn = gameObject.GetComponent<Button>();
    }
    void Update()
    {
        if (stage_Btn.interactable == true)
        {
            timer += Time.deltaTime;

            transform.localScale = new Vector3(Mathf.Cos(timer * scaleSpeed) + size,
                                                Mathf.Cos(timer * scaleSpeed) + size, 0); //ũ�� ���� �ﰢ�Լ��� ���� �ݺ�
        }
        else
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 0.0f);
        }
    }
}
