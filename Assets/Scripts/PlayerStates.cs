using UnityEngine;
using System.Collections;

public abstract class PlayerState {
	public virtual void OnEnter(Player player) { }
	public abstract void OnUpdate(Player player);
	public virtual void OnEnemyCollision(Player player, Collider2D collider) { }
}

public class PlayerSwimming : PlayerState {
	bool facingRight = true;

	// Update is called once per frame
	public override void OnUpdate (Player player) {
		float xDir = Input.GetAxis ("Horizontal");
		float yDir = Input.GetAxis ("Vertical");
		float deadzone = 0.0001f;
		
		if (xDir > deadzone && facingRight != true) {
			player.animator.SetTrigger ("Swim Right");
			facingRight = true;
		} else if (xDir < -deadzone && facingRight == true) {
			player.animator.SetTrigger("Swim Left");
			facingRight = false;
		}
		
		Vector3 movement = new Vector3 (xDir * player.xSpeed, yDir * player.ySpeed);
		player.transform.position += movement * Time.deltaTime;

		GameManager.Instance.FitInBounds(player.transform);
	}

	public override void OnEnemyCollision(Player player, Collider2D collider) {
		player.AddDamage(1);
	}
}

public class PlayerHit : PlayerState {
	float waitTime = 0.5f;

	public override void OnEnter (Player player) {
		player.animator.SetTrigger("Hit");
	}
	
	public override void OnUpdate(Player player) {
		waitTime -= Time.deltaTime;
		if (waitTime <= float.Epsilon) {
			player.SetState(new PlayerDying());
		}
	}
}

public class PlayerDying : PlayerState {
	float descendSpeed = 4f;

	public override void OnEnter (Player player) {
		player.animator.SetTrigger("Dying");
	}
	
	public override void OnUpdate (Player player) {
		Vector3 movement = new Vector3 (0, -descendSpeed);
		player.transform.position += movement * Time.deltaTime;

		if (!player.GetComponent<Renderer>().isVisible) {
			Debug.Log("TODO: Trigger GameOver screen.");
		}
	}
}
