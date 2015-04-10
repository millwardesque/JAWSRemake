using UnityEngine;
using System.Collections;
using GameExtensionMethods;

public class UnderwaterLevel {
	public string levelName;
	public string backgroundImageName;
	public Bounds playBounds;

	public UnderwaterLevel(string name, string backgroundImageName, Bounds playBounds) {
		this.levelName = name;
		this.backgroundImageName = backgroundImageName;
		this.playBounds = playBounds;
	}

	public bool IsInPlayBounds(Vector3 point) {
		return playBounds.Contains(point);
	}

	public void FitInPlayBounds(Transform pos, bool ignoreX = false) {
		Vector3 newPosition = pos.position;

		if (!ignoreX) {
			if (pos.position.x < playBounds.GetLeft()) {
				newPosition.x = playBounds.GetLeft();
			}
			else if (pos.position.x > playBounds.GetRight()) {
				newPosition.x = playBounds.GetRight();
			}
		}
		
		if (pos.position.y < playBounds.GetBottom()) {
			newPosition.y = playBounds.GetBottom();
		}
		else if (pos.position.y > playBounds.GetTop()) {
			newPosition.y = playBounds.GetTop();
		}
		
		pos.position = newPosition;
	}
}
