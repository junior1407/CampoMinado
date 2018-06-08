using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    int x, y;
    public  enum Direction
    {
        UP,DOWN, LEFT,RIGHT   
    }
    int flagsRemaining;
    public int[,] matrix;
    int size;

    public Player(int size)
    {
        matrix = new int[size, size];
        this.size = size;
        this.flagsRemaining = size;
        x = 0;
        y = 0;
    }
    public Vector3 getPosition()
    {
        return new Vector3(x, 1.5f, y);
    }

    public bool Move(Direction d)
    {
        int i=0, j=0;
        switch (d)
        {
            case Direction.UP: { i = x - 1; j = y; }break;
            case Direction.DOWN: { i = x + 1; j = y; } break;
            case Direction.LEFT: { i = x; j = y-1; } break;
            case Direction.RIGHT: { i = x; j = y+1; } break;

        }
        if( i >=0 && i<size && j >= 0 && j < size)
        {
            x = i;
            y = j;
            return true;
        }
        return false;
    }

    public void PlaceFlag()
    {
        if (flagsRemaining>0)
        {
            if (matrix[x,y] ==0)
            {
                Debug.Log("Sucesso");
                flagsRemaining--;
                matrix[x, y] = 1;
            }
        }
    }

}
