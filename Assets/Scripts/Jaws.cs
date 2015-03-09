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
	public int health = 60;

	float xDir = 1.0f;
	bool facingRight = true;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();

		if (target == null) {
			Debug.LogError("Unable to start Jaws: Target isn't set.");
		}
	}

	void Start() {
		GameManager.Instance.JawsHealthUpdated(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (target.IsAlive()) {
			float yDir = (target.transform.position.y - transform.position.y) > float.Epsilon ? 1f : -1f;
			
			Vector3 movement = new Vector3 (xDir * xSpeed, yDir * ySpeed);
			transform.position += movement * Time.deltaTime;

			GameManager.Instance.FitInBounds(transform, true);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Reversal") {
			if (facingRight) {
				facingRight = false;
				xDir = -1f;
				animator.SetTrigger("Swim Left");
			}
			else {
				facingRight = true;
				xDir = 1f;
				animator.SetTrigger("Swim Right");
			}
		}

		if (col.tag == "Bullet") {
			AddDamage(col.GetComponent<Bullet>().damage);
		}
	}

	public void AddDamage(int damage) {
		health -= damage;
		GameManager.Instance.JawsHealthUpdated(this);
	}
}
