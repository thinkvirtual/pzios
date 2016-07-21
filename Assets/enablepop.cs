using UnityEngine;
using System.Collections;

public class enablepop : MonoBehaviour {
    public GameObject keyref;
   public bool reloadkeyboard = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        if (keyref.activeSelf)
        {
            reloadkeyboard = true;
            keyref.SetActive(false);
           // keyref.transform.position -= new Vector3(0, 0, 4);
        }
        else
        {
            reloadkeyboard = false;
        }
    }

    void OnDisable()
    {
        if (reloadkeyboard)
        {
            keyref.SetActive(true);
            reloadkeyboard = false;
           // keyref.transform.position += new Vector3(0, 0, 4);
        }
    }
}
