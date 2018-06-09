using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    /*
    public GameObject tilePrefab;
    public GameObject playerPrefab;
    public GameObject group;*/
    public GameObject playerObj;
    public Helper helper;
    int size = 10;
    Board board;
    Player player;

    public float time = 0f;
    public GameObject[,] tiles;
    public GameObject[,] topTiles;
    void Start()
    {
        board = new Board(size); //Instancia e prepara a matriz.
        board.printMatrix();
        player = new Player(size);
        playerObj = GameObject.Instantiate(helper.playerPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
        tiles = new GameObject[size, size];
        CreateBoard();
    }

    public void CreateBoard()
    {

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {

                tiles[i, j] = GameObject.Instantiate(helper.tilePrefab, new Vector3(i, 0, j), Quaternion.identity, helper.group.transform);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        helper.setTimer((int)time);
        PlayerInput();
    }

    void PlayerInput()
    {
        Player.Direction direction;
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Player.Direction.UP;

            player.Move(direction);
            playerObj.transform.position = player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Player.Direction.LEFT;
            player.Move(direction);
            playerObj.transform.position = player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Player.Direction.DOWN;
          
            player.Move(direction);
            playerObj.transform.position = player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Player.Direction.RIGHT;
            player.Move(direction);
            playerObj.transform.position = player.getPosition();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.PlaceFlag();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // int i = player.
            if (board.isCellBomb(player) == 1)
            {
                GameOver();
            }
            else
            {
                Debug.Log(board.getCellContect(player)+"");
            }
        }

    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}

