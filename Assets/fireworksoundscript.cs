using UnityEngine;
using System.Collections;

public class fireworksoundscript : MonoBehaviour {

	public AudioClip onBirthsound;
	public AudioClip onDeathSound;
	public int numberofparticles=0;
	ParticleSystem partsys;
	public int dispcnt = 0;
	AudioSource myaudio;
	// Use this for initialization
	void Start () {
		partsys = GetComponent<ParticleSystem> ();
		myaudio = GetComponent<AudioSource> ();
	}
	bool played=false;
	
	// Update is called once per frame
	void Update () {


		int count = partsys.particleCount;
		dispcnt = count;
		//if (count <= numberofparticles) {
		//	if (onDeathSound!=null) {
		//		if (!myaudio.isPlaying) {
		//			GetComponent<AudioSource> ().PlayOneShot (onDeathSound);
		//		}
		//	}
		if ((count >= numberofparticles)&&!played) {
			played = true;
			if (onBirthsound != null) {
				if (!myaudio.isPlaying) {
					GetComponent<AudioSource> ().PlayOneShot (onBirthsound);
				}
			}
		} else {
			played = false;
		}
		//numberofparticles = count;
	}
}
