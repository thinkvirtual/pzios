using UnityEngine;
using System.Collections;

public class coconutsound : MonoBehaviour {
    public bool soundplayed = false;
    public AudioClip fallsound;
    public AudioSource ad;

    // Use this for initialization
    void Start () {
        ad = GetComponent<AudioSource>();
	}
	
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.name.Contains("Terrain")) {
        if (!soundplayed)
        {
            ad.PlayOneShot(fallsound);
            soundplayed = true;
        }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
