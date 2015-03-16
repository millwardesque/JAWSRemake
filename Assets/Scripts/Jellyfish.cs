using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Jellyfish : Enemy {
	void Update() {
		if (IsAlive ()) {
			if (GameManager.Instance.bounds.max.y < transform.position.y) {
				Destroy (gameObject);
			}
			transform.position += speed * Time.deltaTime;
		}
	}
}
