using UnityEngine;
using System.Collections;
using GameExtensionMethods;

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
		float yPos = gameManager.CurrentLevel.playBounds.GetBottom() - yPosOffset;
		float xPos = gameManager.CurrentLevel.playBounds.GetRandomX();
		
		if (!facingRight) {
			enemy.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		
		enemy.transform.position = new Vector3(xPos, yPos);
		return enemy;
	}
}
