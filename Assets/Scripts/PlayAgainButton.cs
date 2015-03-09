using UnityEngine;
using System.Collections;

public class PlayAgainButton : MonoBehaviour {

	public void PlayAgain() {
		Application.LoadLevel("sandbox");
	}
}
