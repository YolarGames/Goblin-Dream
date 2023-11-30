using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GoToMainMenu : MonoBehaviour
	{
		[SerializeField] private Button _gotoMenu;
		private SceneBundleLoader _bundleLoader;

		private void Awake()
		{
			_bundleLoader = FindObjectOfType<SceneBundleLoader>();
		}

		private void Start()
		{
			_gotoMenu.onClick.AddListener(_bundleLoader.StartMainMenuLoading);
			_gotoMenu.onClick.AddListener(AudioManager.Instance.PlayClick);
		}
	}
}
