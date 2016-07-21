using UnityEngine;
using System.Collections;

public class escapelogic : MonoBehaviour {
    public GameObject pausemenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)){
            toggleMenu(pausemenu);
        }
	}

    public void toggleMenu(GameObject obj)
    {
        if (obj.activeSelf)
        {
           // Time.timeScale = 1.5f;
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
            //Time.timeScale = 0f;

        }
    }
}
