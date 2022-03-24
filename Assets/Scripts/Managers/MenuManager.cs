using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _settingsMenu;

	[Space(10)]
	[SerializeField] private Button _play;
	[SerializeField] private Button _settings;
	[SerializeField] private Button _back;
	[SerializeField] private Toggle _musicToggle;
	[SerializeField] private Toggle _sfxToggle;
	private AnimationController _menuAnimController;
	private AnimationController _settingsAnimController;

	public static void LoadMainMenuScene()
	{
		// TODO: Show loading screen
		SceneManager.LoadScene("MainMenu");
	}

	private void Start()
	{
		_play.onClick.AddListener(LoadGameScene);
		_settings.onClick.AddListener(SwitchToSettingsMenu);
		_back.onClick.AddListener(SwitchToMainMenu);
		
 		_musicToggle.onValueChanged.AddListener(ToggleMusic);
        _sfxToggle.onValueChanged.AddListener(ToggleSfx);

		_menuAnimController = new AnimationController(_mainMenu);
		_settingsAnimController = new AnimationController(_settingsMenu);
	}

	private void ToggleMusic(bool isOn)
	{
		AudioManager.Instance.ToggleMusicActivity(isOn);
		AudioManager.Instance.PlayClick();
	}

	private void ToggleSfx(bool isOn)
	{
		AudioManager.Instance.ToggleSfxActivity(isOn);
		AudioManager.Instance.PlayClick();
	}

	private void LoadGameScene()
	{
		AudioManager.Instance.PlayClick();
		
		// TODO: Show loading screen 
		// TODO: Load resources from server
		// TODO: Check whether there is proper resources on device
		// TODO: Save resources to device
		
		SceneManager.LoadScene("Game");
	}

	private void SwitchToSettingsMenu()
	{
		AudioManager.Instance.PlayClick();
		
		_settingsMenu.SetActive(true);
		
		_play.enabled = false;
		_settings.enabled = false;
		_back.enabled = true;
		_musicToggle.enabled = true;
		_sfxToggle.enabled = true;
		
		_settingsAnimController.PlayAnimClip("Show");
		_menuAnimController.PlayAnimClip("Hide");
	}

	private void SwitchToMainMenu()
	{
		AudioManager.Instance.PlayClick();
		
		_play.enabled = true;
		_settings.enabled = true;
		_back.enabled = false;
		_musicToggle.enabled = false;
		_sfxToggle.enabled = false;
		
		_settingsAnimController.PlayAnimClip("Hide");
		_menuAnimController.PlayAnimClip("Show");
	}
}
