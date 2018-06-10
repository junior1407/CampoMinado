using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour {

    public InputField nameInput;
    public Text winOrLoseText;
    public Text top10Text;
    public Data save;
    public Record[] records;
    public GameObject ScoreCanvas;
    public GameObject Top10Canvas;

	void Start () {
        try { save = GameObject.FindGameObjectWithTag("Player").GetComponent<Data>(); }
        catch(NullReferenceException e) // fins de teste.
        {
            save = gameObject.AddComponent<Data>();
            save.size = 10;
            save.r = new Record { dateTime = "Teste", Name = "Teste", Score = 0, Time = 0 };
        }
        if (save.r.Score == save.size)
        {
            winOrLoseText.text = "Você ganhou!!!";
        }
        else
        {
            winOrLoseText.text = "Você perdeu :(";
        }
	}
	
    public void Submit()
    {
        save.r.Name = nameInput.text;
        StartCoroutine(PostRequest(save.r));
    }

    public IEnumerator GetRequest()
    {
        UnityWebRequest uwr = UnityWebRequest.Get("http://apiminesweeper.azurewebsites.net/api/gettop10");
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Erro:" + uwr.error);
        }
        else
        {
            records = JsonHelper.FromJson<Record>(uwr.downloadHandler.text);
            int counter=1;
            foreach (Record r in records)
            {
                top10Text.text += counter + " - "+r.Name+" - "+r.Score+" pontos - "+r.Time+" segundos \n";
                counter++;
            }

        }
    }

    public void Restart()
    {
        save.Restart();
    }

    public IEnumerator PostRequest(Record r)
    {
        UnityWebRequest request= new UnityWebRequest("http://apiminesweeper.azurewebsites.net/api/Records", "POST");
      //  UnityWebRequest request = new UnityWebRequest("http://localhost:53606/api/Records", "POST");
        string json = JsonUtility.ToJson(r);
        Debug.Log(json);
        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.isNetworkError)
        {
            Debug.Log("Erro:" + request.error);
        }
        else
        {
            Debug.Log("Sucesso: " + request.downloadHandler.text);
        }
        ScoreCanvas.SetActive(false);
        Top10Canvas.SetActive(true);
        StartCoroutine(GetRequest());

    }
}
