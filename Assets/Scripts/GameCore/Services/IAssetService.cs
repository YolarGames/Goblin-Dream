using Coins;
using UnityEngine;

namespace GameCore.Services
{
	public interface IAssetService : IService
	{
		GameObject Player { get; }
		Coin Coin { get; }
	}
}