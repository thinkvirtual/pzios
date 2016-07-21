using UnityEngine;
using System.Collections;

public class onetimeinstruction : MonoBehaviour {
    public bool actionExecuted = false;
    public bool showonce = false;
    public GameObject msgbox;
    public GameObject target;
    // Use this for initialization
    void Start () {
	   
	}


    public void showInstruction()
    {
        if (!actionExecuted) { 
        actionExecuted = true;
            msgbox.SetActive(true);
        }
        else if(!Example_Reward.hidekeyboard)
        {
            showonce = true;
            target.SetActive(true);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
