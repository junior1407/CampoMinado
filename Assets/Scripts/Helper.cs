using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    public GameObject tilePrefab;
    public  GameObject playerPrefab;
    public  GameObject group;
    public Text timer;
    public Text flagsRemaining;


    public void setTextFlagsRemaining()
    {

    }
    public void setTimer(int seconds)
    {
        int minutes = seconds / 60;
        int sec = seconds % 60;
        string textMinutes = minutes+"";
        if (minutes < 10)
        {
            textMinutes = "0" + minutes;
        }

        string textSeconds = sec+"";
        if (sec < 10)
        {
            textSeconds = "0" + sec;
        }
        timer.text = textMinutes + ":" + textSeconds;

    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
