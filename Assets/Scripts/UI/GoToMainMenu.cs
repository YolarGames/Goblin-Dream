using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class GoToMainMenu : MonoBehaviour
	{
		[SerializeField] private Button _gotoMenu;

	
		private void Start()
		{
			_gotoMenu.onClick.AddListener(LoadingManager.Instance.LoadMainMenuScene);
		}
	}
}
