using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isec = false;
    public bool isDrag;
    Rigidbody2D rigid;
    Vector3 mousePos;

   /* void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }*/

    void Update()
    {
        
    }

    /*public void Drag()
    {
            isDrag = true;
            rigid.simulated = false;
    }

    public void Drop()
    {
        isDrag = false;
        rigid.simulated = true;
    }*/

    public void OnMouseDown()
    {
        isec = true;
    }
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);
    }
    public void OnMouseUp()
    {
        isec = false;
    }
}
