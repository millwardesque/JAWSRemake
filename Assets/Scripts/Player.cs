using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
public class Player : MonoBehaviour {
	[HideInInspector]
	public Animator animator;
	public Gun gun;

	public int lives = 3;
	public float xSpeed = 5f;
	public float ySpeed = 4f;

	int health = 1;
	PlayerState currentState;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator> ();

		if (gun == null) {
			Debug.LogError("Unable to start Player: Gun is null.");
		}
	}

	void Start() {
		Initialize();
	}

	public void Initialize() {
		SetState(new PlayerSwimming());
		health = 1;
		transform.position = Vector3.zero;
	}

	public void FireGun(bool facingRight) {
		gun.FireGun(facingRight);
	}

	public void SetState(PlayerState newState) {
		if (newState != null) {
			currentState = newState;
			currentState.OnEnter(this);
		}
		else {
			Debug.LogError ("Unable to set player state: New state is null.");
		}
	}

	// Update is called once per frame
	void Update () {
		if (currentState != null) {
			currentState.OnUpdate(this);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Enemy" && currentState != null) {
			currentState.OnEnemyCollision(this, collider);
		}
	}

	public void AddDamage(int damage) {
		health -= damage;

		if (!IsAlive ()) {
			SetState(new PlayerHit());
		}
	}

	public bool IsAlive() {
		return health > 0;
	}

	public void LoseLife() {
		lives--;
		GameManager.Instance.OnPlayerLostLife();
	}
}
