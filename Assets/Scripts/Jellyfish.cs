using UnityEngine;
using System.Collections;
using GameExtensionMethods;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Jellyfish : Enemy {
	void Update() {
		if (IsAlive ()) {
			if (GameManager.Instance.CurrentLevel.playBounds.GetTop() < transform.position.y) {
				Destroy (gameObject);
			}
			transform.position += speed * Time.deltaTime;
		}
	}
}
