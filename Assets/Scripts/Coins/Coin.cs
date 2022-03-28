using UnityEngine;
using Utils;

public class Coin : MonoBehaviour
{
	public int coinValue;
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private BoxCollider _boxCollider;
	[SerializeField] private float _rotationAngle;
	[SerializeField] private float _destroyCoinAfter;
	[SerializeField] private AudioClip _coinClip;
	
	private const float DestroyTime = 5f;

	private void Start()
	{
		if (_destroyCoinAfter != 0f)
			Destroy(gameObject, _destroyCoinAfter);
	}

	private void Update()
	{
		if (!_rigidbody.isKinematic)
			return;

		RotateCoin();
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(Tags.player))
		{
			CoinCollector.AddScore(coinValue);
			Destroy(gameObject);
		}
		
		if (collision.gameObject.CompareTag(Tags.ground))
		{
			_rigidbody.isKinematic = true;
			_boxCollider.isTrigger = true;
			Destroy(gameObject, DestroyTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag(Tags.player))
			return;

		CoinCollector.AddScore(coinValue);
		AudioManager.Instance.PlaySfxClip(_coinClip);
		Destroy(gameObject);
	}

	private void RotateCoin()
	{
		var rotation = Quaternion.AngleAxis(_rotationAngle * Time.deltaTime, Vector3.up);
		transform.rotation *= rotation;
	}
}
