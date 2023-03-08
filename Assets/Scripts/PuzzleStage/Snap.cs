using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap
{
    public Vector3[] BlockGroupPos = new Vector3[6];
    public Snap()
    {
        BlockGroupPos[0] = new Vector3(-2.8f, -3.36f, 0);
        BlockGroupPos[1] = new Vector3(-1.68f, -3.36f, 0);
        BlockGroupPos[2] = new Vector3(-0.56f, -3.36f, 0);
        BlockGroupPos[3] = new Vector3(0.56f, -3.36f, 0);
        BlockGroupPos[4] = new Vector3(1.68f, -3.36f, 0);
        BlockGroupPos[5] = new Vector3(2.8f, -3.36f, 0);
    }
}
