using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Player : MonoBehaviour {
	Animator animator;
	bool facingRight = true;
	public float xSpeed = 1f;
	public float ySpeed = 1f;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float xDir = Input.GetAxis ("Horizontal");
		float yDir = Input.GetAxis ("Vertical");
		float deadzone = 0.0001f;

		if (xDir > deadzone && facingRight != true) {
			animator.SetTrigger ("Swim Right");
			facingRight = true;
		} else if (xDir < -deadzone && facingRight == true) {
			animator.SetTrigger("Swim Left");
			facingRight = false;
		}

		Vector3 movement = new Vector3 (xDir * xSpeed, yDir * ySpeed);
		transform.position += movement * Time.deltaTime;
	}
}
