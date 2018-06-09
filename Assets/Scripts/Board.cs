using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board {

    public char[,] matrix;
    public string line;
    int SIZE;

    public Board(int size)
    {
        this.SIZE = size;
        matrix = new char[SIZE, SIZE];
        addBombsMatrix();
        fillMatrix();
    }

  
    void addBombsMatrix()
    {
        int x, y;
        for (int i = 0; i< SIZE; i++)
        {
            x = UnityEngine.Random.Range(0, SIZE); // SIZE não incluso.
            y = UnityEngine.Random.Range(0, SIZE);
            if (matrix[x,y] == '*')
            {
                i--;
            }
            matrix[x,y] = '*';
        }
    }
    void fillMatrix()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (matrix[i,j] != '*')
                {
                    matrix[i, j] = GetAdjacentBombs(i, j);
                }
            }
        }

    }
    
    public char GetAdjacentBombs(int i, int j)
    {
        int sum = (isCellBomb(i - 1, j - 1) + isCellBomb(i, j - 1) + isCellBomb(i + 1, j - 1)
            + isCellBomb(i - 1, j) + isCellBomb(i + 1, j)
            + isCellBomb(i - 1, j + 1) + isCellBomb(i , j + 1) + +isCellBomb(i +1, j+1));
        return sum.ToString().ToCharArray()[0]; 
    }

    public int isCellBomb(Player p)
    {
        Vector3 pos = p.getPosition();
        return isCellBomb((int)pos.x, (int) pos.z);
    }

    public int getCellContect(Player p)
    {
        Vector3 pos = p.getPosition();
        return getCellContect((int)pos.x, (int)pos.z);
    }
    public int isCellBomb(int i, int j)
    {
        if (i < 0 || i >= SIZE || j<0 || j>=SIZE)
        {
            return 0;
        }
        if (matrix[i,j] == '*') { return 1; }
        return 0;
    }
    public int getCellContect(int i, int j)
    {
        return (int) char.GetNumericValue(matrix[i, j]);
    }
   
    public void printMatrix()
    {
        for (int i=0; i < SIZE; i++) 
        {
            line = string.Empty;
            for(int j=0; j<SIZE; j++)
            { 
                line += matrix[i, j] + ",";
            }
            Debug.Log(line + "");
        }
    }

    public bool checkFlags(int[,] flags)
    {
        int counter = 0;
        for(int i=0; i<SIZE; i++)
        {
            for (int j =0; j< SIZE; j++)
            {
                if (matrix[i,j] == '*' && flags[i,j] == 1)
                {
                    counter++;
                }
            }
        }
        if (counter == SIZE) { return true; }
        return false;
    }
}
