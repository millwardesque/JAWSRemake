using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public Bullet bulletPrefab;

	void Awake() {
		if (bulletPrefab == null) {
			Debug.LogError ("Unable to start gun: No bullet prefab is set.");
		}
	}

	public void FireGun(bool facingRight) {
		Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Bullet;
		if (!facingRight) {
			bullet.speed *= -1f;
			bullet.transform.localScale *= -1f;
		}
	}
}
