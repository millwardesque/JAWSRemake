using UnityEngine;
using System.Collections;

public class JellyfishManager : EnemySpawner<Jellyfish> {
	static public JellyfishManager Instance;
	
	protected override void Awake() {
		base.Awake();
		
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy (gameObject);
		}
	}
	
	protected override Jellyfish Spawn() {
		GameManager gameManager = GameManager.Instance;
		Jellyfish enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Jellyfish;
		enemy.transform.SetParent(transform);

		bool facingRight = (Random.Range(0, 2) == 0);
		float yPosOffset = 0;	// Use an offset to move the enemy just slightly offscreen.
		float yPos = gameManager.bounds.center.y - gameManager.bounds.extents.y - yPosOffset;
		float xPos = Random.Range(gameManager.bounds.center.x - gameManager.bounds.extents.x, gameManager.bounds.center.x + gameManager.bounds.extents.x);
		
		if (!facingRight) {
			enemy.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		
		enemy.transform.position = new Vector3(xPos, yPos);
		return enemy;
	}
}
