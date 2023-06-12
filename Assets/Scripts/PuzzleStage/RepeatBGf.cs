using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatBGf : MonoBehaviour
{
    [SerializeField] [Range(0f, 100f)] float speed;
    [SerializeField] float posValue;

    Vector2 startPos;  // Vector3�� Vector2�� ����

    float newPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        newPos = (Mathf.Repeat(Time.time * speed, posValue));

        // Vector3.right�� Vector2.right�� ����
        transform.position = -(startPos + Vector2.right * newPos);
    }
}
