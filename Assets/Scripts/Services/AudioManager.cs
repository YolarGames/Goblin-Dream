using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Services
{
	public class AudioManager : MonoBehaviourSingleton<AudioManager>
	{
		[SerializeField] private AudioSource _musicSource;
		[SerializeField] private AudioSource _sfxSource;
		[SerializeField] private AudioMixer _audioMixer;
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

		public void SetMusic(AudioClip audioClip)
		{
			_musicSource.clip = audioClip;
			_musicSource.Play();
		}
	}
}