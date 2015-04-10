using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public Slider jawsHealthSlider;
	public Text scoreText;
	public Text shellText;
	public RectTransform statusPanel;
	public GameObject gameOverPanel;
	public GameObject levelStartPanel;
	public Text levelStartLives;
	public Text levelStartHighScore;
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

			if (statusPanel == null) {
				Debug.LogError("Unable to start GUI Manager: No status panel is set.");
			}

			if (gameOverPanel == null) {
				Debug.LogError("Unable to start GUI Manager: No game-over panel is set.");
			}

			if (levelStartPanel == null) {
				Debug.LogError("Unable to start GUI Manager: No level-start panel is set.");
			}

			if (levelStartLives == null) {
				Debug.LogError("Unable to start GUI Manager: No level-start lives text is set.");
			}

			if (levelStartHighScore == null) {
				Debug.LogError("Unable to start GUI Manager: No level-start high-score text is set.");
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

	public void ShowLevelStartPanel() {
		levelStartPanel.SetActive(true);
		levelStartLives.text = string.Format ("Lives: {0}", GameManager.Instance.player.lives);
		levelStartHighScore.text = string.Format("Hi-score: {0}", 10000); // @TODO Replace with real high score.
	}
	
	public void HideLevelStartPanel() {
		levelStartPanel.SetActive(false);
	}

	public RectTransform GetStatusPanel() {
		return statusPanel;
	}
}
