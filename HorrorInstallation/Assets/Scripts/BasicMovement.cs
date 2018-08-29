using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BasicMovement : MonoBehaviour {
	public enum gameState {Normal, GVR, Other};
	public gameState currentState;
	public float Movementspeed, turnMoveSpeed;
	public float turnspeed, reverseSpeed, hitIterations;
	private float verticalAxis, horizontalAxis;
	private float currentRotation;
	private Rigidbody RB;
	public Transform Camera;
	private bool canhurt = true;
	public int keys = 0, hp = 2;
	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody> ();
		currentRotation = this.transform.position.y;

	}
	
	/**Void Update is called once per frame, and FixedUpdate is called at a unit of time set in Unity.
	 * Update can be called at a different rate on different computers, because they are playing the game at different frame rates.
	 * Because of this, anything to do with Unity physis should go in FixedUpdate, so it is consistant between computers.
	 * This also means FixedUpdate cannot call if Unity's time(time.timeScale) is set to 0, but Update will call(many functions within Update won't though, but code such as anything resetting the timeScale to 1f will)
	 * */
	void Update () {
		if (hp == 0) {
			SceneManager.LoadScene ("Test Scene");
		}
		horizontalAxis = Input.GetAxis("Horizontal");
		verticalAxis = Input.GetAxis ("Vertical");
		/**
		currentRotation += horizontalAxis * turnspeed;

		this.transform.eulerAngles = new Vector3 (this.transform.rotation.x,
			currentRotation, this.transform.rotation.z);
		*/
	}

	void FixedUpdate(){
		/**
		 * Turn and move were created for and explained in Unity's official tank game tutorial
		 * 
		 * */

		Turn ();
		Move ();

		if (Input.GetKeyDown (KeyCode.Space)) {

		}

	}
	void Turn(){
		float turn = turnspeed * horizontalAxis * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
		RB.MoveRotation (RB.rotation * turnRotation);
	}
	void Move(){
		float mod=0f, move=1f;
		Vector3 movement = transform.forward * verticalAxis * Movementspeed * Time.deltaTime;
		if (verticalAxis > 0f) {
			mod = 1f;
		} else {
			mod = reverseSpeed;
		}
		if (horizontalAxis == 0f) {
			move = 1f;
		} else {
			move = turnMoveSpeed;
		}
		RB.MovePosition (RB.position + (movement*mod*move));
	}

	void OnCollisionEnter(Collision other){
		if (other.transform.tag == "Killer"&& canhurt) {
			StartCoroutine (hit ());
			canhurt = false;
		}
	}
	IEnumerator hit(){
		Camera.Rotate(0f, 0f, (hitIterations*-1f));
		for (int i = 0; i <(int)hitIterations; i++) {
			yield return null;
			Camera.Rotate (0f, 0f, 1f);
		}
		hp--;
		canhurt = true;
	}
	public int playerKeyCount(){
		return keys;
	}
	public void getNewKey(){
		keys++;
	}
}
