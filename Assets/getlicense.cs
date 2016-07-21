using UnityEngine;
using LitJson;
using System;
using System.Collections;

public class parseJSON
{
    public string code;
}
public class getlicense : MonoBehaviour
{
    public string currentEntry = "";
    public GameObject validator;
    // Sample JSON for the following script has attached.
    IEnumerator Proc()
    {
        string url = " URL of the JSON to be Decode";
        WWW www = new WWW("http://magnesium.pythonanywhere.com/mydemo");
        yield return www;
        if (www.error == null)
        {
            Processjson(www.data);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }

    public void Start()
    {

        StartCoroutine("Proc");
    }

    private void Processjson(string jsonString)
    {
        JsonData jsonvale = JsonMapper.ToObject(jsonString);
        parseJSON parsejson;
        parsejson = new parseJSON();
        parsejson.code = jsonvale["enabled"].ToString();

        if (!parsejson.code.Contains("true")){
            Application.Quit();

        }
        else
        {
            Application.LoadLevel(1);
        }
 //parsejson.id = jsonvale["ID"].ToString();

        //parsejson.but_title = new ArrayList ();
        //parsejson.but_image = new ArrayList ();

        //for(int i = 0; i<jsonvale["buttons"].Count; i++)
        //{
        //	parsejson.but_title.Add(jsonvale["buttons"][i]["title"].ToString());
        //		parsejson.but_image.Add(jsonvale["buttons"][i]["image"].ToString());
        //	}    
    }
}