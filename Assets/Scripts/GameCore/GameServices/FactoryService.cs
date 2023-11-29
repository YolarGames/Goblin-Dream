using System.Threading.Tasks;
using Coins;
using UnityEngine;

namespace GameCore.GameServices
{
	public sealed class FactoryService : IFactoryService
	{
		private readonly IAssetService _assetService;
		public GameObject Player;

		public FactoryService(IAssetService assetService) =>
			_assetService = assetService;

		public Task Init() =>
			Task.CompletedTask;

		public void CreatePlayer(Vector3 position) =>
			Player = Object.Instantiate(_assetService.Player, position, Quaternion.identity);

		public Coin CreateCoin(Vector3 position) =>
			Object.Instantiate(_assetService.Coin, position, Quaternion.identity) as Coin;
	}
}