using System.Collections;
using GameCore.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Coins
{
	public class CoinSpawner : MonoBehaviour
	{
		[SerializeField] private Vector3 _spawnBounds;
		[SerializeField] private float _spawnCooldown;
		private Vector3 _spawnArea;
		private IFactoryService _factoryService;
		private WaitForSeconds _waitForIt;
		private Coroutine _coroutine;

		private void Awake()
		{
			_factoryService = GameServices.FactoryService;
			_spawnArea = transform.position + _spawnBounds;
			_waitForIt = new WaitForSeconds(_spawnCooldown);
		}

		private void Start() =>
			_coroutine = StartCoroutine(SpawnCoins());

		private void OnDisable() =>
			StopCoroutine(_coroutine);

		private IEnumerator SpawnCoins()
		{
			while (enabled)
			{
				Vector3 spawnPoint = GetRandomSpawnPoint();
				Coin coinInstance = _factoryService.CreateCoin(spawnPoint);
				coinInstance.transform.parent = transform;

				yield return _waitForIt;
			}
		}
		
		private Vector3 GetRandomSpawnPoint()
		{
			var randomX = Random.Range(-_spawnArea.x, _spawnArea.x);
			var randomZ = Random.Range(-_spawnArea.z, _spawnArea.z);

			return new Vector3(randomX, _spawnArea.y, randomZ);
		}
	}
}
