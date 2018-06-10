using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {

    public Player player;
    int size = 10;
    public Record r;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
