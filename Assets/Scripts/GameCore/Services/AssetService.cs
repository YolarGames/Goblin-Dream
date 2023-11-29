using System.Threading.Tasks;
using Coins;
using Configs;
using UnityEngine;

namespace GameCore.Services
{
	public sealed class AssetService : IAssetService
	{
		private AssetServiceConfig _assetServiceConfig;

		public AssetService(IConfigService configService) =>
			_assetServiceConfig = configService.AssetServiceConfig;

		public GameObject Player=> _assetServiceConfig.PlayerPrefab;
		public Coin Coin => _assetServiceConfig.CoinPrefab;

		public Task Init() =>
			Task.CompletedTask;
	}
}