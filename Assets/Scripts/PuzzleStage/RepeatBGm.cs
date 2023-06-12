using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBGm : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] float speed;
    [SerializeField] float posValue;

    Vector2 startPos;  // Vector3를 Vector2로 변경

    float newPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        newPos = (Mathf.Repeat(Time.time * speed, posValue));

        // Vector3.right를 Vector2.right로 수정
        transform.position = -(startPos + Vector2.right * newPos);
    }
}
