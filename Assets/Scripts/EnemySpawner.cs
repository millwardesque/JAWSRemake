using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EnemySpawner<T> : 
	MonoBehaviour
		where T : Enemy
{
	public T prefab;
	public int maxEnemies = 1;
	public float spawnDelay = 1f;
	
	float currentDelay = 0f;
	List<T> activeEnemies;
	
	protected virtual void Awake() {
		if (null == prefab) {
			Debug.LogError ("Unable to start enemey manager for '" + typeof(T) + ": No prefab is set.");
		}

		activeEnemies = new List<T>();
		currentDelay = spawnDelay;
	}
	
	void Update() {
		CleanupDeadEnemies();
		
		if (activeEnemies.Count < maxEnemies) {
			currentDelay -= Time.deltaTime;
			if (currentDelay <= 0f) {
				currentDelay = spawnDelay;
				T newEnemy = Spawn ();
				if (newEnemy != null) {
					activeEnemies.Add(newEnemy);
				}
			}
		}
	}
	
	public void KillAllEnemies() {
		List<T> deadlist = new List<T>();
		foreach (T enemy in activeEnemies) {
			deadlist.Add(enemy);
		}
		
		foreach (T enemy in deadlist) {
			Destroy(enemy.gameObject);
			activeEnemies.Remove(enemy);
		}
		activeEnemies.Clear ();
	}
	
	void CleanupDeadEnemies() {
		List<T> deadlist = new List<T>();
		foreach (T enemy in activeEnemies) {
			if (enemy == null) {
				deadlist.Add(enemy);
			}
		}
		
		foreach (T enemy in deadlist) {
			activeEnemies.Remove(enemy);
		}
	}

	protected abstract T Spawn();
}
