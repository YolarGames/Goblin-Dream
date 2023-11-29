using System;
using UnityEngine;

namespace Utils
{
	[Serializable]
	public class Cooldown
	{
		[SerializeField] private float _cooldownValue;
		private float _cooldownCompleteTime;

		public void Reset() => _cooldownCompleteTime = Time.time + _cooldownValue;
		public bool IsReady => _cooldownCompleteTime <= Time.time;
	}
}