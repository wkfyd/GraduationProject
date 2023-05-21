using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Image image;
    public void EnterColor()
    {
        image.color = new Color(0, 255, 255, 0.2f);
    }

    public void ExitColor()
    {
        image.color = new Color(255, 255, 255, 0);
    }
}
