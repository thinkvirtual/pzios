using UnityEngine;
using System.Collections;

public class timeddeactivator : MonoBehaviour {
    public ParticleSystem s;
    public float tme=3;
	// Use this for initialization
	void Start () {
	}

    void OnEnable()
    {
        Invoke("DeactivateMe", tme);
    }
    
    void DeactivateMe()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
