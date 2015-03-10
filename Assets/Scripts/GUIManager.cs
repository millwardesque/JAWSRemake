using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public Slider jawsHealthSlider;
	public Text scoreText;
	public Text shellText;
	public GameObject gameOverPanel;
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

			if (shellText == null) {
				Debug.LogError("Unable to start GUI Manager: No Shell text is set.");
			}

			if (shellText == null) {
				Debug.LogError("Unable to start GUI Manager: No game-over panel is set.");
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

	public void UpdateShells(int shells) {
		shellText.text = shells.ToString();
	}

	public void ShowGameOverPanel() {
		gameOverPanel.SetActive(true);
	}

	public void HideGameOverPanel() {
		gameOverPanel.SetActive(false);
	}
}
