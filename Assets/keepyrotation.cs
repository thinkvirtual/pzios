using UnityEngine;
using System.Collections;

public class keepyrotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, transform.rotation.z,1f);

	}
}
