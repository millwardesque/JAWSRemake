using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameExtensionMethods;

public class GameManager : MonoBehaviour {
	public GameObject leftOutOfBounds;
	public GameObject rightOutOfBounds;
	public SpriteRenderer background;
	public Shell shellPrefab;

	UnderwaterLevel currentLevel;
	public UnderwaterLevel CurrentLevel {
		get { return currentLevel; }
	}

	List<UnderwaterLevel> underwaterLevels = new List<UnderwaterLevel>();
	float defaultChanceToSpawnShell = 0.8f;
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

	[HideInInspector]
	public Player player;

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

			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			if (player == null) {
				Debug.LogError ("Unable to start Game Manager: Couldn't find an object with the player tag.");
			}

			// Load the underwater level data.
			underwaterLevels.Add(new UnderwaterLevel(
				"shallow", 
				"jaws-shallow", 
				new Bounds(new Vector3(0f, -1.055f), new Vector3(10f, 3.55f))
			));
			underwaterLevels.Add(new UnderwaterLevel(
				"deep",
				"jaws-deep",
				new Bounds(new Vector3(0f, -6.65f), new Vector3(10f, 14.66f))
			));
		}
		else {
			Destroy (gameObject);
		}
	}

	void Start() {
		PlayerScore = 0;
		InitLevel ("deep");
	}

	public void PickRandomLevel() {
		int levelIndex = Random.Range(0, underwaterLevels.Count);
		InitLevel (underwaterLevels[levelIndex]);
	}

	public void InitLevel(string levelName) {
		UnderwaterLevel level = underwaterLevels.Find (x => x.levelName == levelName);
		if (level != null) {
			InitLevel (level);
		}
		else {
			Debug.LogError("Unable to init level '" + levelName + "': No level with that name was found.");
		}
	}

	public void InitLevel(UnderwaterLevel level) {
		if (level != null) {
			currentLevel = level;
			StartCoroutine(IntroLevel (level));
		}
		else {
			Debug.LogError("Unable to init level: Level is null.");
		}
	}

	IEnumerator IntroLevel(UnderwaterLevel level) {
		background.sprite = Resources.Load<Sprite>(level.backgroundImageName);

		chanceToSpawnShell = defaultChanceToSpawnShell;
		StingrayManager.Instance.KillAllEnemies();
		StingrayManager.Instance.maxEnemies = 0;
		player.Initialize();

		GUIManager.Instance.ShowLevelStartPanel();
		yield return new WaitForSeconds(0);
		GUIManager.Instance.HideLevelStartPanel();
		StingrayManager.Instance.maxEnemies = 1;
	}
	
	public void JawsHealthUpdated(Jaws jaws) {
		GUIManager.Instance.UpdateJawsHealth(jaws.health);

		if (!jaws.IsAlive()) {
			PlayerWins();
		}
	}

	public void OnPlayerLostLife() {
		if (player.lives > 0) {
			PlayerShells = (PlayerShells > 1 ? PlayerShells / 2 : PlayerShells);
			PickRandomLevel();
		}
		else {
			GUIManager.Instance.ShowGameOverPanel();
		}
	}

	public void PlayerWins() {
		Application.LoadLevel ("Ending");
	}

	public void OnEnemyDies(Enemy enemy) {
		PlayerScore += enemy.scoreValue;
		
		if (Random.value < chanceToSpawnShell) {
			SpawnShell(enemy.transform.position);
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
