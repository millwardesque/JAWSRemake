using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StingrayManager : MonoBehaviour {
	public Stingray stingrayPrefab;
	public int maxRays = 1;
	public float spawnDelay = 1f;

	public static StingrayManager Instance;

	float currentDelay = 0f;
	List<Stingray> activeStingrays;

	void Awake() {
		if (null == Instance) {
			Instance = this;

			if (null == stingrayPrefab) {
				Debug.LogError ("Unable to start Stingray Manager: No stingray prefab is set.");
			}

			activeStingrays = new List<Stingray>();
			currentDelay = spawnDelay;
		}
		else {
			Destroy (gameObject);
		}
	}

	void Update() {
		CleanupDeadStingrays();

		if (activeStingrays.Count < maxRays) {
			currentDelay -= Time.deltaTime;
			if (currentDelay <= 0f) {
				currentDelay = spawnDelay;
				SpawnStingray();
			}
		}
	}

	public void KillAllStingrays() {
		List<Stingray> deadlist = new List<Stingray>();
		foreach (Stingray stingray in activeStingrays) {
			deadlist.Add(stingray);
		}
		
		foreach (Stingray stingray in deadlist) {
			Destroy(stingray.gameObject);
			activeStingrays.Remove(stingray);
		}
		activeStingrays.Clear ();
	}

	void CleanupDeadStingrays() {
		List<Stingray> deadlist = new List<Stingray>();
		foreach (Stingray stingray in activeStingrays) {
			if (stingray == null) {
				deadlist.Add(stingray);
			}
		}
		
		foreach (Stingray stingray in deadlist) {
			activeStingrays.Remove(stingray);
		}
	}

	void SpawnStingray() {
		GameManager gameManager = GameManager.Instance;
		Stingray stingray = Instantiate(stingrayPrefab, Vector3.zero, Quaternion.identity) as Stingray;
		stingray.transform.SetParent(transform);
		bool facingRight = (Random.Range(0, 2) == 0);
		float xPosOffset = 2f;	// Use an offset to move the stingray out of it's starting out-of-bounds position, otherwise it'll get caught in the trigger and be destroyed.
		float yPos = Random.Range(gameManager.bounds.center.y - gameManager.bounds.extents.y, gameManager.bounds.center.y + gameManager.bounds.extents.y);
		float xPos = GameManager.Instance.leftOutOfBounds.transform.position.x + xPosOffset;

		if (!facingRight) {
			stingray.transform.localScale = new Vector3(-1f, 1f, 1f);
			stingray.speed *= -1f;
			xPos = GameManager.Instance.rightOutOfBounds.transform.position.x - xPosOffset;
		}

		stingray.transform.position = new Vector3(xPos, yPos);
		activeStingrays.Add (stingray);
	}
}
