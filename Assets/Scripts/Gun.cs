using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {
	public Bullet bulletPrefab;
	public int maxBullets = 3;
	List<Bullet> activeBullets = new List<Bullet>();

	void Awake() {
		if (bulletPrefab == null) {
			Debug.LogError ("Unable to start gun: No bullet prefab is set.");
		}
	}

	public bool CanFireBullet() {
		return activeBullets.Count < maxBullets;
	}

	public void FireGun(bool facingRight) {
		if (!CanFireBullet()) {
			return;
		}

		Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Bullet;
		if (!facingRight) {
			bullet.speed *= -1f;
			bullet.transform.localScale *= -1f;
		}
		activeBullets.Add(bullet);
	}

	void Update() {
		// Clean up the dead bullets.
		List<Bullet> deadList = new List<Bullet>();
		foreach (Bullet bullet in activeBullets) {
			if (bullet == null) {
				deadList.Add(bullet);
			}
		}

		foreach (Bullet bullet in deadList) {
			activeBullets.Remove(bullet);
		}
	}
}
