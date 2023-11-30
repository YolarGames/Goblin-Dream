using Services;
using TMPro;
using UnityEngine;

namespace Coins
{
	public class CoinCollector : MonoBehaviour
	{
		[SerializeField] private TMP_Text _coinText;
		private static int _totalCoins;
		
		private void Start() =>
			GameEventManager.OnPickupCoin += UpdateCoinCounter;

		private void OnDisable() =>
			GameEventManager.OnPickupCoin -= UpdateCoinCounter;
		
		public static void AddScore(int coinValue)
		{
			_totalCoins += coinValue;
			GameEventManager.InvokePickUpCoin();
		}

		private void UpdateCoinCounter() =>
			_coinText.SetText(_totalCoins.ToString());
	}
}
