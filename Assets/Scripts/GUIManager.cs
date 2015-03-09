using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public Slider jawsHealthSlider;
	public static GUIManager Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);

			if (jawsHealthSlider == null) {
				Debug.LogError("Unable to start GUI Manager: No JAWS Health Slider is set.");
			}
		}
		else {
			Destroy (gameObject);
		}
	}

	public void UpdateJawsHealth(int newHealth) {
		jawsHealthSlider.value = (float)newHealth;
	}
}
