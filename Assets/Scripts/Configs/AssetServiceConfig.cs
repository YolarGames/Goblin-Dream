using Coins;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(fileName = "AssetServiceConfig", menuName = "Configs/Asset Service Config")]
	public class AssetServiceConfig : ScriptableObject, IConfig
	{
		public Coin CoinPrefab;
		public GameObject PlayerPrefab;
	}
}