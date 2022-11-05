using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleScript : MonoBehaviour
{
    [Header("scale speed")]  //인스펙터창 제목
    [Range(1f, 10f)] public float size = 1f;   //오브젝트 크기
    [Range(1f, 10f)] public float scaleSpeed = 1f; //크기변경 속도
    private Button stage_Btn;
    float timer = 0;
    void Start()
    {
        stage_Btn = gameObject.GetComponent<Button>();
    }
    void Update()
    {
        if (stage_Btn.interactable == true)
        {
            timer += Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Cos(timer * scaleSpeed) + size,
                                                Mathf.Cos(timer * scaleSpeed) + size, 0); //크기 증감 삼각함수로 인한 반복
        }
        else
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 0.0f);
        }
    }
}
