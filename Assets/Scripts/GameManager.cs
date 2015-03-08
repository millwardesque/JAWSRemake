using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Bounds bounds;
	public SpriteRenderer background;

	public static GameManager Instance;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);

			if (background == null) {
				Debug.LogError("Unable to start Game Manager: Background is null.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start() {
		InitLevel ("jaws-shallow");
	}

	public void InitLevel(string bgName) {
		background.sprite = Resources.Load<Sprite>(bgName);
		this.bounds = bounds;
	}
}
