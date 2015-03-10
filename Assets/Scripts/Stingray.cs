using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Stingray : MonoBehaviour {
	public int health = 3;
	public float speed = 3.0f;
	public float delayAfterHit = 0.2f;
	float currentHitDelay = 0f;

	// Use this for initialization
	void Start () {
	
	}

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

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Out-of-bounds") {
			Destroy (gameObject);
		}
		else if (col.tag == "Bullet") {
			AddDamage(col.GetComponent<Bullet>().damage);
			currentHitDelay = delayAfterHit;
		}
	}

	public void AddDamage(int damage) {
		health -= damage;

		if (!IsAlive()) {
			Destroy (gameObject);
		}
	}
	
	public bool IsAlive() {
		return health > 0;
	}
}
