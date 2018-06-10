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
    public  GameObject group;
    public InputField inputName;
    public Text timer;
    public Data dados;
    public Text flagsRemaining;
    public Material blue;
    

    public IEnumerator PostRequest(Record r)
    {
        //UnityWebRequest request= new UnityWebRequest("http://apiminesweeper.azurewebsites.net/api/Records", "POST");
        UnityWebRequest request = new UnityWebRequest("http://localhost:53606/api/Records", "POST");



        string json = JsonUtility.ToJson(r);
        Debug.Log(json);
        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Error While Sending: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
        }
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

    internal void setTextFlagsRemaining(int v)
    {
        flagsRemaining.text = v + "";
    }
}
