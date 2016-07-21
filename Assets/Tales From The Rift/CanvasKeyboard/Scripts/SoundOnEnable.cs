using UnityEngine;
using System.Collections;

public class SoundOnEnable : MonoBehaviour {
    public AudioClip popupsound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        GetComponent<AudioSource>().PlayOneShot(popupsound);

    }
}
