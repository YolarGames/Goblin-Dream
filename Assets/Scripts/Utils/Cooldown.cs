using System;
using UnityEngine;

namespace Utils
{
	[Serializable]
	public class Cooldown
	{
		[SerializeField] private float _value = 1f;
		private float _cooldownCompleteTime;

		public void Reset() => _cooldownCompleteTime = Time.time + _value;
		public bool IsReady => _cooldownCompleteTime <= Time.time;
	}
}