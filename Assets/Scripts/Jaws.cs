using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Jaws : MonoBehaviour {
	public Player target;

	Animator animator;
	public float xSpeed = 4.0f;
	public float ySpeed = 3.0f;
	bool facingRight = true;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();

		if (target == null) {
			Debug.LogError("Unable to start Jaws: Target isn't set.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target.IsAlive()) {
			float xDir = 0f;
			float yDir = (target.transform.position.y - transform.position.y) > float.Epsilon ? 1f : -1f;

			if (facingRight != true) {
				xDir = -1f;

			} else {
				xDir = 1f;
			}
			
			Vector3 movement = new Vector3 (xDir * xSpeed, yDir * ySpeed);
			transform.position += movement * Time.deltaTime;
		}
	}
}
