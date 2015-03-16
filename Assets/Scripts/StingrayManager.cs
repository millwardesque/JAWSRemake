using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StingrayManager : EnemySpawner<Stingray> {
	static public StingrayManager Instance;

	protected override void Awake() {
		base.Awake();

		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}

	protected override Stingray Spawn() {
		GameManager gameManager = GameManager.Instance;
		Stingray stingray = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Stingray;
		stingray.transform.SetParent(transform);
		bool facingRight = (Random.Range(0, 2) == 0);
		float xPosOffset = 2f;	// Use an offset to move the stingray out of it's starting out-of-bounds position, otherwise it'll get caught in the trigger and be destroyed.
		float yPos = Random.Range(gameManager.bounds.center.y - gameManager.bounds.extents.y, gameManager.bounds.center.y + gameManager.bounds.extents.y);
		float xPos = GameManager.Instance.leftOutOfBounds.transform.position.x + xPosOffset;

		if (!facingRight) {
			stingray.transform.localScale = new Vector3(-1f, 1f, 1f);
			stingray.speed *= -1f;
			xPos = GameManager.Instance.rightOutOfBounds.transform.position.x - xPosOffset;
		}

		stingray.transform.position = new Vector3(xPos, yPos);
		return stingray;
	}
}
