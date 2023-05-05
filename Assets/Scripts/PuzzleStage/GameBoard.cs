using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard
{
    public Vector3[,] blockGridPos = new Vector3[9, 6];
    public GameBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                blockGridPos[x, y] = new Vector3(1.12f * y - 2.8f , 4.48f - 1.12f * x , 0);
            }
        }
    }
}
