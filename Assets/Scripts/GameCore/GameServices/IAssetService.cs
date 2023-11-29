using Coins;
using UnityEngine;

namespace GameCore.GameServices
{
	public interface IAssetService : IService
	{
		GameObject Player { get; }
		Coin Coin { get; }
	}
}