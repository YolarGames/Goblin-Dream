using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[Space(10)]
		[SerializeField] private Button _play;
		[SerializeField] private Button _settings;
		[SerializeField] private Toggle _musicToggle;
		[SerializeField] private Toggle _sfxToggle;
		[SerializeField] private Button _back;
		
		private readonly int _menuAnimHash = Animator.StringToHash("showMenu");
		private readonly int _settingsAnimHash = Animator.StringToHash("showSettings");
		
		private void Awake() =>
			_animator = GetComponent<Animator>();

		private void OnEnable()
		{
			_play.onClick.AddListener(LoadGameScene);
			_settings.onClick.AddListener(SwitchToSettingsMenu);
			
			_musicToggle.onValueChanged.AddListener(ToggleMusic);
			_sfxToggle.onValueChanged.AddListener(ToggleSfx);
			_back.onClick.AddListener(SwitchToMainMenu);
		}

		private void OnDisable()
		{
			_play.onClick.RemoveListener(LoadGameScene);
			_settings.onClick.RemoveListener(SwitchToSettingsMenu);
			
			_musicToggle.onValueChanged.RemoveListener(ToggleMusic);
			_sfxToggle.onValueChanged.RemoveListener(ToggleSfx);
			_back.onClick.RemoveListener(SwitchToMainMenu);
		}

		private void SwitchToSettingsMenu()
		{
			AudioManager.Instance.PlayClick();
			_animator.Play(_settingsAnimHash);
		}

		private void SwitchToMainMenu()
		{
			AudioManager.Instance.PlayClick();
			_animator.Play(_menuAnimHash);
		}

		private void ToggleMusic(bool isOn)
		{
			AudioManager.Instance.PlayClick();
			AudioManager.Instance.ToggleMusicActivity(isOn); 
		}

		private void ToggleSfx(bool isOn)
		{
			AudioManager.Instance.PlayClick();
			AudioManager.Instance.ToggleSfxActivity(isOn);
		}

		private void LoadGameScene()
		{
			AudioManager.Instance.PlayClick();
			LoadingManager.Instance.LoadGameScene();
		}
	}
}
