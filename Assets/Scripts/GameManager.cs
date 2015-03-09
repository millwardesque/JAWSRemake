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
		InitLevel ("jaws-shallow", new Bounds(new Vector3(0f, -1.055f), new Vector3(10f, 3.55f)));
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
}
