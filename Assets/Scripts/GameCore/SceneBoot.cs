using Services;
using UnityEngine;

namespace GameCore
{
	public class SceneBoot : MonoBehaviour
	{
		[SerializeField] private AudioClip _sceneClip;
	
		private void Awake() =>
			Application.targetFrameRate = 60;

		private void Start() =>
			AudioManager.Instance.SetMusic(_sceneClip);
	}
}