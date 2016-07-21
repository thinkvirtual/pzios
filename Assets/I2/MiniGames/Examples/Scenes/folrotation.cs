using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class folrotation : MonoBehaviour {
    public RectTransform lp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, lp.rotation.z,1);
	}
}
