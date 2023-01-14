using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool select = false;
    Vector3 mousePos;

    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        select = true;
    }
    public void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드좌표 마우스 위치
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);
    }
    public void OnMouseUp()
    {
        select = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     
    }
}
