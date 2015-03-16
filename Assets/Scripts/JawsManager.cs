using UnityEngine;
using System.Collections;

public class JawsManager : EnemySpawner<Jaws> {
	static public JawsManager Instance;
	
	protected override void Awake() {
		base.Awake();
		
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}
	
	protected override Jaws Spawn() {
		GameManager gameManager = GameManager.Instance;
		Jaws enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Jaws;
		enemy.transform.SetParent(transform);
		bool facingRight = (Random.Range(0, 2) == 0);
		float xPosOffset = 2f;	// Use an offset to move the enemy out of it's starting out-of-bounds position, otherwise it'll get caught in the trigger and be destroyed.
		float yPos = Random.Range(gameManager.bounds.center.y - gameManager.bounds.extents.y, gameManager.bounds.center.y + gameManager.bounds.extents.y);
		float xPos = gameManager.leftOutOfBounds.transform.position.x + xPosOffset;
		
		if (!facingRight) {
			enemy.transform.localScale = new Vector3(-1f, 1f, 1f);
			enemy.speed.x *= -1f;
			xPos = gameManager.rightOutOfBounds.transform.position.x - xPosOffset;
		}

		enemy.target = gameManager.player;
		enemy.transform.position = new Vector3(xPos, yPos);
		return enemy;
	}
}
