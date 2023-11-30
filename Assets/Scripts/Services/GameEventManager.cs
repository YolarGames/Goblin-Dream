using System;
using Utils;

namespace Services
{
	public static class GameEventManager
	{
		public static event Action OnPickupCoin;
		
		public static void InvokePickUpCoin() => OnPickupCoin?.Invoke();
	}
}