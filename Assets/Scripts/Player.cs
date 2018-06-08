using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    int x, y;
    int flagsRemaining;
    public char[,] matrix;
    public List<Vector2Int> flags;
    int size;

    public Player(int size)
    {
        this.size = size;
        this.flagsRemaining = size;
        x = 0;
        y = 0;
    }

}
