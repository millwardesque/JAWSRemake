using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameExtensionMethods;

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
		Stingray enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Stingray;
		enemy.transform.SetParent(transform);
		bool facingRight = (Random.Range(0, 2) == 0);
		float xPosOffset = 2f;	// Use an offset to move the stingray out of it's starting out-of-bounds position, otherwise it'll get caught in the trigger and be destroyed.
		float yPos = gameManager.CurrentLevel.playBounds.GetRandomY();
		float xPos = gameManager.leftOutOfBounds.transform.position.x + xPosOffset;

		if (!facingRight) {
			enemy.transform.localScale = new Vector3(-1f, 1f, 1f);
			enemy.speed.x *= -1f;
			xPos = gameManager.rightOutOfBounds.transform.position.x - xPosOffset;
		}

		enemy.transform.position = new Vector3(xPos, yPos);
		return enemy;
	}
}
