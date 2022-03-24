using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
	[SerializeField] private float _cooldownValue;
	private float _cooldownCompleteTime;

	
	public void Reset() => _cooldownCompleteTime = Time.time + _cooldownValue;
	public bool isReady => _cooldownCompleteTime <= Time.time;
}