using System;
using UnityEngine;

public class SceneBoot : MonoBehaviour
{
	[SerializeField] private AudioClip _sceneClip;
	
	private void Start()
	{
		AudioManager.Instance.SetMusic(_sceneClip);
	}
}
