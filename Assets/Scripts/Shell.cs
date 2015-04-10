using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class Shell : MonoBehaviour {
	public float descentSpeed = 1f;
	public int scoreValue = 100;
	public float lifetime = 12f;
	float fadeTime = 0f;

	SpriteRenderer spriteRenderer;

	void Awake() {
		fadeTime = lifetime / 4f;

		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.Instance.CurrentLevel.IsInPlayBounds(transform.position)) {
			return;
		}

		transform.position -= new Vector3(0f, descentSpeed * Time.deltaTime);
		GameManager.Instance.CurrentLevel.FitInPlayBounds(transform);
		lifetime -= Time.deltaTime;
		if (lifetime < fadeTime) {
			Color currentColor = spriteRenderer.color;
			currentColor.a = lifetime / fadeTime;
			spriteRenderer.color = currentColor;
		}

		if (lifetime <= 0f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			GameManager.Instance.PlayerScore += scoreValue;
			GameManager.Instance.PlayerShells++;
			Destroy (gameObject);
		}
	}
}
