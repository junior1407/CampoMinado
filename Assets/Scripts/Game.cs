using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    
    public GameObject playerObj;
    public Helper helper;
    int size = 10;
    Board board;
    int movementEnabled = 1;
    public float time = 0f;
    public GameObject[,] tiles;
    bool[,] revealed;
    public GameObject[,] topTiles;
    void Start()
    {
        board = new Board(size); //Instancia e prepara a matriz.
        board.printMatrix();
        helper.dados.player = new Player(size);
        playerObj = GameObject.Instantiate(helper.playerPrefab, new Vector3(0, 1.5f, 0), Quaternion.identity);
        helper.playerAnimator = playerObj.GetComponent<Animator>();
        tiles = new GameObject[size, size];
        topTiles = new GameObject[size, size];
        revealed = new bool[size, size];
        CreateBoard();
    }

    public bool isRevealed(Player p)
    {
        Vector3 pos = p.getPosition();
        return revealed[(int) pos.x, (int) pos.z];
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

        if (movementEnabled == 1)
        {
            PlayerInput();
        }
    }

    void PlayerInput()
    {
        Player.Direction direction;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Player.Direction.UP;

            helper.dados.player.Move(direction);
            playerObj.transform.position = helper.dados.player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Player.Direction.LEFT;
            helper.dados.player.Move(direction);
            playerObj.transform.position = helper.dados.player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Player.Direction.DOWN;

            helper.dados.player.Move(direction);
            playerObj.transform.position = helper.dados.player.getPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Player.Direction.RIGHT;
            helper.dados.player.Move(direction);
            playerObj.transform.position = helper.dados.player.getPosition();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 pos = helper.dados.player.getPosition();
            int i = (int)pos.x;
            int j = (int)pos.z;

            if (helper.dados.player.PlaceFlag() && (!revealed[i, j]))
            {
               
                topTiles[i,j] =  GameObject.Instantiate(helper.flagPrefab, new Vector3(i, 1.5f, j), Quaternion.identity);
                helper.setTextFlagsRemaining(helper.dados.player.getFlagsRemaining());
                if (helper.dados.player.getFlagsRemaining() ==0) { CheckWinorLose(); }
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((helper.dados.player.isFlagged()==0) && !isRevealed(helper.dados.player))
            StartCoroutine(Dig());
        }

    }

    IEnumerator RevealAllBombs()
    {
        for(int i=0; i< size; i++)
        {
            for (int j=0; j< size; j++)
            {
                if (board.isCellBomb(i,j)==1)
                {
                    topTiles[i, j] = GameObject.Instantiate(helper.bombPrefab, new Vector3(i, 1.5f, j), Quaternion.identity);
                }
            }
        }
        yield return new WaitForSeconds(2.0f);
    }
    IEnumerator Dig()
    {
        movementEnabled = 0;
        helper.playerAnimator.SetTrigger("dig");

         yield return new WaitForSeconds(1f);
        if (board.isCellBomb(helper.dados.player) == 1)
        {

            yield return StartCoroutine(RevealAllBombs());
            CheckWinorLose();
        }
        else
        {
            RevealCell(helper.dados.player);
        }
        movementEnabled = 1;
        yield return new WaitForSeconds(2.0f);
    }
 

    void RevealCell(Player player)
    {
        Vector3 pos = player.getPosition();
        int i = (int)pos.x;
        int j = (int)pos.z;
        RevealCell(i, j);
    }

    void RevealCell(int x, int y)
    {

        if (x >= 0 && x < size && y >= 0 && y < size)
        {
            if (revealed[x, y] == false)
            {
                revealed[x, y] = true;

                if (board.isCellBomb(x, y) == 0)
                {
                    int content = board.getCellContect(x, y);
                    if (content == 0)
                    {
                         tiles[x, y].transform.Translate(Vector3.down*0.5f);
                         RevealCell(x - 1, y - 1);
                        RevealCell(x - 1, y);
                        RevealCell(x - 1, y + 1);
                        RevealCell(x, y - 1);
                        RevealCell(x, y + 1);
                        RevealCell(x + 1, y - 1);
                        RevealCell(x + 1, y);
                        RevealCell(x + 1, y + 1);
                    }
                    else
                    {
                        tiles[x, y].GetComponentInChildren<TextMesh>().text = content + "";
                    }
                }
            }
        }
    }


   
    private void CheckWinorLose()
    {

        int score = board.checkFlags(helper.dados.player.getMatrix());
        int finalTime = (int) time;
        DateTime date = DateTime.Now;
        Debug.Log(date.ToString());
       helper.dados.r= new Record { Id = 1, dateTime = date.ToString(), Score = score, Time = finalTime };

        SceneManager.LoadScene("ScoreScene", LoadSceneMode.Single);
       
    }


    private void Win()
    {
        Debug.Log("Win");
        throw new NotImplementedException();
    }

    private void GameOver()
    {
        Debug.Log("Loss");
        throw new NotImplementedException();
    }
}

