using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Stingray : Enemy {
	void Update() {
		if (IsAlive ()) {
			if (currentHitDelay <= float.Epsilon) {
				transform.position += new Vector3 (speed * Time.deltaTime, 0f);
			}
			else {
				currentHitDelay -= Time.deltaTime;
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Out-of-bounds") {
			Destroy (gameObject);
		}
		else {
			base.OnTriggerEnter2D(col);
		}
	}
}
