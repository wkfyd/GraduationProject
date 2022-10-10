using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage01 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("클릭");
        }
        
    }
    public void OnClicked()
    {
        Debug.Log("클릭");
    }
}
