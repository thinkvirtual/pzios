using UnityEngine;
using System.Collections;

public class punchsc : MonoBehaviour {
   public  GameObject mnuroot;
    public GameObject helpMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void punchme()
    {

    }

    void OnHover()
    {

        iTween.PunchScale(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 1f);

    }
}
