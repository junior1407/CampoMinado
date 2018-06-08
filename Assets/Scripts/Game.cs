using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    int size = 10;
    Board board;
    Player player;
	void Start () {
        board = new Board(size); //Instancia e prepara a matriz.
        board.printMatrix();
        player = new Player(size);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
