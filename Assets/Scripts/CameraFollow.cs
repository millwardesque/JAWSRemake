using UnityEngine;
using System.Collections;
using GameExtensionMethods;

[RequireComponent (typeof(Camera))]
public class CameraFollow : MonoBehaviour {
	public float followHeight = 1f;	// Height of border around the camera. When the player crosses this border, the camera will scroll.
	public float uiHeight = 2.1f;	// Height of the UI at the bottom of the screen.  Used because otherwise the camera doesn't scroll far enough.
	Transform target;
	Camera myCamera;


	void Awake() {
		myCamera = GetComponent<Camera>();
	}


	void Start() {
		target = GameManager.Instance.player.transform;
	}

	void LateUpdate() {
		if (target != null) {
			Vector3 newPosition = transform.position;
			float cameraBottom = transform.position.y - myCamera.orthographicSize;
			if (cameraBottom + followHeight > target.position.y && cameraBottom > GameManager.Instance.CurrentLevel.playBounds.GetBottom() - uiHeight) {
				newPosition.y = target.position.y + myCamera.orthographicSize - followHeight;
			}
			else if (transform.position.y + myCamera.orthographicSize - followHeight < target.position.y) {
				newPosition.y = target.position.y - myCamera.orthographicSize + followHeight;
			}

			transform.position = newPosition;
		}
	}
}
