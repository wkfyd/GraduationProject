using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    [Header("scale speed")]
    [Range(1f, 5f)] public float scaleSpeed = 1f;

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + scaleSpeed * Time.deltaTime,
                            transform.localScale.y + scaleSpeed * Time.deltaTime, 0);

        /*timer += Time.deltaTime;    //삼각함수 기록용
        transform.localScale = new Vector3(10.0f * Mathf.Cos(timer), 10.0f * Mathf.Cos(timer), 0);*/
    }
}
