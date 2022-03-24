using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
	[SerializeField] private TMP_Text _coinText;
	[SerializeField] private ParticleSystem _pickUpParticles;
	private static int _totalCoins;

	public static void AddScore(int coinValue)
	{
		_totalCoins += coinValue;
		EventManager.Instance.PickUpCoin();
	}

	private void UpdateCoinCounter()
	{
		_coinText.text = _totalCoins.ToString();
		_pickUpParticles.Play();
	}
	
	private void Start()
	{
		EventManager.Instance.onPickupCoin += UpdateCoinCounter;
	}

	private void OnDisable()
	{
		EventManager.Instance.onPickupCoin -= UpdateCoinCounter;
	}
}
