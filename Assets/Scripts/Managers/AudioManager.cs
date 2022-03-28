using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _sfxSource;
	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private AudioClip _musicMainMenu;
	[SerializeField] private AudioClip _musicInBattle;
	[SerializeField] private AudioClip _clickSound;

	public void PlayClick()
	{
		PlaySfxClip(_clickSound);
	}
	
	public void PlaySfxClip(AudioClip clip)
	{
		_sfxSource.clip = clip;
		_sfxSource.Play();
	}
	public void ToggleMusicActivity(bool toggleActive)
	{
		_audioMixer.SetFloat("musicVolume", toggleActive ? 0 : -80);
	}

	public void ToggleSfxActivity(bool toggleActive)
	{
		_audioMixer.SetFloat("sfxVolume", toggleActive ? 0 : -80);
	}

	private void Start()
	{
		SetMainMenuMusic();
		SceneManager.sceneLoaded += ChangeMusic;
	}

	private void ChangeMusic (Scene scene, LoadSceneMode loadMode)
	{
		if (scene.name == "Game")
			SetBattleMusic();

		if (scene.name == "MainMenu")
			SetMainMenuMusic();
	}

	private void SetMainMenuMusic()
	{
		_musicSource.clip = _musicMainMenu;
		_musicSource.Play();
	}
	
	private void SetBattleMusic()
	{
		_musicSource.clip = _musicInBattle;
		_musicSource.Play();
	}
}