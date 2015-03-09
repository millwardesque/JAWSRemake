using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed = 6f;
	public int damage = 1;
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(speed * Time.deltaTime, 0);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Enemy" || col.tag == "Reversal") { // @TODO Rename the Reversal tag to something more appropriate now that we're using it for bullet destruction as well as Jaws' direction reversal
			Destroy(gameObject);
		}
	}
}
