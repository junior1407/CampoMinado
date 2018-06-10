using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    public GameObject tilePrefab;
    public  GameObject playerPrefab;
    public Animator playerAnimator;
    public GameObject flagPrefab;
    public GameObject bombPrefab;
    public  GameObject group;
    public InputField inputName;
    public Text timer;
    public Data dados;
    public Text flagsRemaining;
    public Material blue;
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

    internal void setTextFlagsRemaining(int v)
    {
        flagsRemaining.text = v + "";
    }
}
