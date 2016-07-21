using UnityEngine;
using System.Collections;

public class kinematiconbutton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Rigidbody[] tmp=GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody current in tmp)
            {
                current.isKinematic = false;
            }
        }
	}
}
