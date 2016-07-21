using UnityEngine;
using System.Collections;

public class recent : MonoBehaviour {
	// Use this for initialization
    public Transform camtransform;
    public Transform targettrans;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            camtransform.LookAt(targettrans);
            
        }
	}
}
