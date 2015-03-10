using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public Slider jawsHealthSlider;
	public Text scoreText;
	public static GUIManager Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;

			if (jawsHealthSlider == null) {
				Debug.LogError("Unable to start GUI Manager: No JAWS Health Slider is set.");
			}

			if (scoreText == null) {
				Debug.LogError("Unable to start GUI Manager: No Score text is set.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	public void UpdateJawsHealth(int newHealth) {
		jawsHealthSlider.value = (float)newHealth;
	}

	public void UpdateScore(int score) {
		scoreText.text = score.ToString();
	}
}
