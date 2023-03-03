using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap
{
    public Vector3[] gridPos = new Vector3[6];
    public Snap()
    {
        gridPos[0] = new Vector3(-2.8f, -3.36f, 0);
        gridPos[1] = new Vector3(-1.68f, -3.36f, 0);
        gridPos[2] = new Vector3(-0.56f, -3.36f, 0);
        gridPos[3] = new Vector3(0.56f, -3.36f, 0);
        gridPos[4] = new Vector3(1.68f, -3.36f, 0);
        gridPos[5] = new Vector3(2.8f, -3.36f, 0);
    }
}
