using System;
using Utils;

namespace Services
{
	public class EventManager : MonoBehaviourSingleton<EventManager>
	{
		public Action OnPickupCoin;
		
		public void PickUpCoin() => OnPickupCoin?.Invoke();
	}
}