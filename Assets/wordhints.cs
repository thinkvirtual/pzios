using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class wordhints : MonoBehaviour {
    public static List<string> wordtargets = new List<string>();
    public Text txtcomp;
	// Use this for initialization
	void Start () {
        txtcomp = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void drawTargets(List<string> sol)
    {
        string underscorestr="";
        foreach (string c in sol)
        {
            underscorestr += "  ";
            int stringsize = c.Length;
            for(int i=0; i < stringsize; i++)
            {
                underscorestr += " _";
            }


        }

        txtcomp.text = underscorestr;

    }

    public void refreshDisplay(string solution)
    {
        txtcomp.text = solution;
    }
}
