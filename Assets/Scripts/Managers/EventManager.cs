using System;

public class EventManager : MonoBehaviourSingleton<EventManager>
{
	public Action onPickupCoin;
	public void PickUpCoin() => onPickupCoin?.Invoke();
}