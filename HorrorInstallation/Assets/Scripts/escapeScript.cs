using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class escapeScript : MonoBehaviour {
	public enum gameState {Normal, GVR, Other};
	public gameState currentState;
	private int i = 0;
	public int currentKeys;
	public GameObject Player, Lights;
	private bool canEscape = false, leave  = false;
	public KillerScript theKiller;
	public List<GameObject> keyList = new List<GameObject> ();
	void Start(){
		StartCoroutine (setLeave ());
		Lights.SetActive (false);
	}
	IEnumerator setLeave(){
		yield return new WaitForSeconds(.05f);
		currentKeys = i;
		theKiller.recieveTargets (keyList.ToArray ());
		leave = true;
	}
	public void countKeys(GameObject o){
		i++;
		keyList.Add (o);
	}
	public void keyCollected(){
		currentKeys--;
		Debug.Log (currentKeys);
		theKiller.getFaster ();
		if (currentKeys <= 1) {
			theKiller.changeStateHunt ();
		}
	}
	void Update(){
		if (Player.GetComponent<BasicMovement> ().playerKeyCount () == i && leave) {
			canEscape = true;
			Lights.SetActive (true);
			leave = false;
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			
			if (other.GetComponent<BasicMovement> ().playerKeyCount () == i && canEscape) {
				SceneManager.LoadScene ("Test Scene");
			}
		}

	}
}
