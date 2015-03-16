using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class Jaws : Enemy {
	public Player target;

	Animator animator;
	float xDir = 1.0f;
	bool facingRight = true;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void Start() {
		GameManager.Instance.JawsHealthUpdated(this);
		
		if (target == null) {
			Debug.LogError("Unable to start Jaws: Target isn't set.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (IsAlive() && target.IsAlive()) {
			if (currentHitDelay <= float.Epsilon) {
				float yDir = (target.transform.position.y - transform.position.y) > float.Epsilon ? 1f : -1f;
				
				Vector3 movement = new Vector3 (xDir * speed.x, yDir * speed.y);
				transform.position += movement * Time.deltaTime;

				GameManager.Instance.FitInBounds(transform, true);
			}
			else {
				currentHitDelay -= Time.deltaTime;
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D col) {	
		// Change direction if the shark hits the out-of-bounds triggers.
		if (col.tag == "Out-of-bounds") {
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

		base.OnTriggerEnter2D(col);
	}

	protected override void OnDamaged ()
	{
		base.OnDamaged();
		GameManager.Instance.JawsHealthUpdated(this);
	}
}
