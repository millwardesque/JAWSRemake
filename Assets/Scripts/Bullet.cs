using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 6f;
	public int damage = 1;
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(speed * Time.deltaTime, 0);

		if (!GameManager.Instance.IsInBounds(transform.position)) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy") {
			Destroy(gameObject);
		}
	}
}
