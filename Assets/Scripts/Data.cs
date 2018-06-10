using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour {

    public Player player;
    public int size = 10;
    public Record r;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }
}
