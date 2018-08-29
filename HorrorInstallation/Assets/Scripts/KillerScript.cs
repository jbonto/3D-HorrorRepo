using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class KillerScript : MonoBehaviour {
	private NavMeshAgent nMA;
	public float movespeed, patrolDist, playerDist;
	private float curKeyDist, curPlayerDist;
	public GameObject currentKey;
	public Transform Player;
	public enum state {standby, patrol, hunt};
	public state possessedState;
	public GameObject[] targets;
	// Use this for initialization
	void Start () {
		nMA = GetComponent<NavMeshAgent> ();
		nMA.speed = movespeed;
	}
	public void getFaster(){
		movespeed *= 1.07f;
		nMA.speed = movespeed;
	}
	// Update is called once per frame
	void Update () {
		if (possessedState == state.standby) {
			stateStandbyAction ();
		} else if (possessedState == state.patrol) {
			statePatrolAction ();
		} else if (possessedState == state.hunt) {
			stateHuntAction ();
		}
	}
	public void recieveTargets(GameObject[] t){
		targets = t;
		currentKey = targets[Random.Range(0, targets.Length)];
		possessedState = state.patrol;
	}
	void stateStandbyAction(){

	}
	void statePatrolAction(){
		if (currentKey) {
			curKeyDist = Vector3.Distance (this.transform.position, currentKey.transform.position);
		}
		curPlayerDist = Vector3.Distance (this.transform.position, Player.position);
		GameObject tempKey;
		if (curPlayerDist > playerDist && currentKey) {
			nMA.SetDestination (currentKey.transform.position);
		} else if(curPlayerDist <=playerDist) {
			nMA.SetDestination (Player.position);
		}
		if ((curKeyDist < patrolDist) || !currentKey) {
			tempKey = targets[Random.Range(0, targets.Length)];
			while (tempKey == null) {
				tempKey = targets[Random.Range(0, targets.Length)];
			}
			currentKey = tempKey;
		}
	}
	void stateHuntAction(){
		nMA.SetDestination (Player.position);
	}
	public void changeStateStandby(){
		possessedState = state.standby;
	}
	public void changeStatePatrol(){
		possessedState = state.patrol;
	}
	public void changeStateHunt(){
		possessedState = state.hunt;
	}
}
