using Coins;
using UnityEngine;

namespace GameCore.GameServices
{
	public interface IFactoryService: IService
	{
		void CreatePlayer(Vector3 position);
		Coin CreateCoin(Vector3 position);
	}
}