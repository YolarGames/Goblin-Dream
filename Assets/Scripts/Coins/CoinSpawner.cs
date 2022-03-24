using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
	public GameObject[] coinPrefabs;
	[SerializeField] private bool _startOnAwake;
	[SerializeField] private Vector3 _spawnBounds;
	[SerializeField] private Cooldown _spawnCD;
	private Vector3 _spawnArea;

	private void Start()
	{
		_spawnCD.Reset();
		_spawnArea = transform.position + _spawnBounds;
	}

	private void Update()
	{
		SpawnCoins();
	}

	private void SpawnCoins()
	{
		if (!_startOnAwake)
			return;
		
		if (!_spawnCD.isReady)
			return;
		
		var coin = GetRandomCoinObject();
		var spawnPoint = GetRandomSpawnPoint();
		var coinInstance = Instantiate(coin, spawnPoint, Quaternion.identity, this.transform);
		_spawnCD.Reset();
	}

	private GameObject GetRandomCoinObject()
	{
		var randomIndex = Random.Range(0, coinPrefabs.Length);
		return coinPrefabs[randomIndex];
	}

	private Vector3 GetRandomSpawnPoint()
	{
		var randomX = Random.Range(-_spawnArea.x, _spawnArea.x);
		var randomZ = Random.Range(-_spawnArea.z, _spawnArea.z);

		return new Vector3(randomX, _spawnArea.y, randomZ);
	}
}
