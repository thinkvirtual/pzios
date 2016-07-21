using UnityEngine;
using System.Collections;

public class itpop : MonoBehaviour {
    AudioSource las;
	// Use this for initialization
	void Start () {
	
	}
	
    void OnEnable()
    {
        //Invoke("delayPunch", .2f);
        delayPunch();
    }

    public void delayPunch()
    {
        iTween.PunchScale(this.gameObject, new Vector3(2.5f, 2.5f, transform.localScale.z), 1f);

    }
    public void popDone()
    {
        las = GetComponent<AudioSource>();
        las.Play();
    }
    // Update is called once per frame
    void Update () {
	
	}
}
