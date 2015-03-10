using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Bounds bounds;
	public GameObject leftOutOfBounds;
	public GameObject rightOutOfBounds;
	public SpriteRenderer background;
	public Shell shellPrefab;

	float defaultChanceToSpawnShell = 0.1f;
	float chanceToSpawnShell = 0.1f;

	int playerScore = 0;
	public int PlayerScore {
		get { return playerScore; }
		set {
			playerScore = value;
			GUIManager.Instance.UpdateScore(playerScore);
		}
	}

	int playerShells = 0;
	public int PlayerShells {
		get { return playerShells; }
		set {
			playerShells = value;
			GUIManager.Instance.UpdateShells(playerShells);
		}
	}

	public static GameManager Instance;

	// Use this for initialization
	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);

			if (background == null) {
				Debug.LogError("Unable to start Game Manager: Background is null.");
			}

			if (leftOutOfBounds == null) {
				Debug.LogError ("Unable to start Game Manager: Left Out-of-bounds is null.");
			}

			if (rightOutOfBounds == null) {
				Debug.LogError ("Unable to start Game Manager: Right Out-of-bounds is null.");
			}

			if (shellPrefab == null) {
				Debug.LogError ("Unable to start Game Manager: Shell prefab is null.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start() {
		InitLevel ("jaws-shallow", new Bounds(new Vector3(0f, -1.055f), new Vector3(10f, 3.55f)));
		PlayerScore = 0;
		chanceToSpawnShell = defaultChanceToSpawnShell;
	}

	public void InitLevel(string bgName, Bounds levelBounds) {
		background.sprite = Resources.Load<Sprite>(bgName);
		bounds = levelBounds;

		Debug.Log (string.Format ("Level bounds: ({0},{1}), ({2},{3})", bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y));
	}

	public void FitInBounds(Transform pos, bool ignoreX = false) {
		Vector3 newPosition = pos.position;

		if (!ignoreX) {
			if (pos.position.x < (bounds.center.x - bounds.extents.x)) {
				newPosition.x = (bounds.center.x - bounds.extents.x);
			}
			else if (pos.position.x > (bounds.center.x + bounds.extents.x)) {
				newPosition.x = (bounds.center.x + bounds.extents.x);
			}
		}

		if (pos.position.y < (bounds.center.y - bounds.extents.y)) {
			newPosition.y = (bounds.center.y - bounds.extents.y);
		}
		else if (pos.position.y > (bounds.center.y + bounds.extents.y)) {
			newPosition.y = (bounds.center.y + bounds.extents.y);
		}

		pos.position = newPosition;
	}

	public bool IsInBounds(Vector3 point) {
		return bounds.Contains(point);
	}

	public void JawsHealthUpdated(Jaws jaws) {
		GUIManager.Instance.UpdateJawsHealth(jaws.health);

		if (!jaws.IsAlive()) {
			PlayerWins();
		}
	}

	public void PlayerWins() {
		Application.LoadLevel ("Ending");
	}

	public void OnStingrayDies(Stingray stingray) {
		PlayerScore += stingray.scoreValue;


		if (Random.value < chanceToSpawnShell) {
			SpawnShell(stingray.transform.position);
			chanceToSpawnShell = defaultChanceToSpawnShell;
		}
		else {
			chanceToSpawnShell += 0.05f;
		}
	}

	void SpawnShell(Vector3 position) {
		Instantiate(shellPrefab, position, Quaternion.identity);
	}
}
