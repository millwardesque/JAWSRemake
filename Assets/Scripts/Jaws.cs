using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Jaws : MonoBehaviour {
	public GameObject target;
	Animator animator;
	public float xSpeed = 1.0f;
	public float ySpeed = 1.0f;
	public bool facingRight = true;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();

		if (target == null) {
			Debug.LogError("Unable to start Jaws: Target isn't set.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = (target.transform.position - transform.position);

		if (direction.x > float.Epsilon && facingRight != true) {
			animator.SetTrigger ("Swim Right");
			facingRight = true;
		} else if (direction.x < -float.Epsilon && facingRight == true) {
			animator.SetTrigger("Swim Left");
			facingRight = false;
		}
		
		Vector3 movement = new Vector3 (direction.x * xSpeed, direction.y * ySpeed);
		transform.position += movement * Time.deltaTime;
	}
}
