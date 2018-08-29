using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClipOnEnter : MonoBehaviour {
	public AudioClip Soundeffect;
	private AudioSource Audio;
	// Use this for initialization
	void Start () {
		Audio = GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter(Collider other){
		Audio.PlayOneShot(Soundeffect);

	}
	IEnumerator stopSound(){
		yield return new WaitForSeconds (1.8f);
		Audio.Stop ();
	}
}
