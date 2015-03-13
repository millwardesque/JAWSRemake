using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Enemy : MonoBehaviour {
	public int health = 1;
	public float speed = 1f;
	public int scoreValue = 10;
	public float delayAfterHit = 0.05f;
	protected float currentHitDelay = 0f;

	protected virtual void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Bullet") {
			AddDamage(col.GetComponent<Bullet>().damage);
		}
	}

	public void AddDamage(int damage) {
		health -= damage;
		
		if (!IsAlive()) {
			GameManager.Instance.OnEnemyDies(this);
			Destroy (gameObject);
		}
		else {
			currentHitDelay = delayAfterHit;
		}
	}
	
	public bool IsAlive() {
		return health > 0;
	}
}
