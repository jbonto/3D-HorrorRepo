using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (sayName ("jim"));
		StartCoroutine (sayName ("bob"));

	}
	
	IEnumerator sayName(string name){
		int i = 0;
		for (int u = 0; u < 10; u++) {
			yield return new WaitForSeconds (1f);
			Debug.Log (name + u);
		}

	}
}
