using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard
{
    public Vector3[,] blockGridPos = new Vector3[7, 6];
    public GameBoard()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                blockGridPos[x, y] = new Vector3(1.12f * y - 2.8f , 1.12f * x - 3.36f, 0);
            }
        }
    }
}
