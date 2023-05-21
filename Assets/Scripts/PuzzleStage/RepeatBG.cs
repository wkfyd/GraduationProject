using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBG : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float speed = 3f;

    [SerializeField] float posValue;

    Vector3 startPos;
    float newPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, posValue);
        transform.position = startPos + Vector3.right * newPos;
    }
}
