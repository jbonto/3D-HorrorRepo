using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyScript : MonoBehaviour {
	private escapeScript theEscape;
	public enum gameState {Normal, GVR, Other};
	public gameState currentState;
	void Start(){
		theEscape = GameObject.FindGameObjectWithTag ("Escape").GetComponent<escapeScript> ();
		theEscape.countKeys (this.gameObject);
	}
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			other.GetComponent<BasicMovement> ().getNewKey ();
			theEscape.keyCollected ();
			Destroy (this.gameObject);
		}
	}
	public void collected(BasicMovement e){
		e.getNewKey ();
		theEscape.keyCollected ();
		Destroy (this.gameObject);
	}
}
