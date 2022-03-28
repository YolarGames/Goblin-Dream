using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class LoadingManager : MonoBehaviourSingleton<LoadingManager>
{
	[SerializeField] private Text _loadingText;
	[SerializeField] private Text _percentText;
	[SerializeField] private Text _internetWarning;
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private Cooldown _loadingAnimationCD;
	[SerializeField] private Cooldown _internetCheckCD;
	[SerializeField] private Image _internetLogo;

	private AsyncOperationHandle<SceneInstance> _asyncHandlerForAddressables;
	private AsyncOperation _asyncOperation;
	private int _dotsCount;
	private bool _isOnline;

	public void LoadGameScene()
	{
		if (IsFirstTimeOpened() && !_isOnline)
			return;
		
		StartCoroutine(FadeInRoutine());
		_asyncHandlerForAddressables = Addressables.LoadSceneAsync("Game", LoadSceneMode.Single);
		SaveManager.SaveDataToJson(Application.persistentDataPath, Application.version);
	}
	
	public void LoadMainMenuScene()
	{
		StartCoroutine(FadeInRoutine());
		_asyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
	
	private void Start()
	{
		_loadingAnimationCD.Reset();
		_internetCheckCD.Reset();
		SetDefaultCanvasGroupSettings();
		
		SceneManager.sceneLoaded += AudioManager.Instance.ChangeMusic;
		SceneManager.sceneLoaded += SetFirstLaunchToFalse;
	}

	private void LateUpdate()
	{
		IsAppConnectedToInternet();
		UpdateSceneLoadStatus();
		UpdateSceneLoadStatusForAddressables();
		SwitchLoadingText();
	}

	private void UpdateSceneLoadStatus()
	{
		if (_asyncOperation == null)
			return;
			
		if (_asyncOperation.progress.Equals(1f))
		{
			StopCoroutine(FadeInRoutine());
			StartCoroutine(FadeOutRoutine());
		}

		_percentText.text = _asyncOperation.progress.ToString("P0");
	}
	
	private void UpdateSceneLoadStatusForAddressables()
	{
		if (!_asyncHandlerForAddressables.IsValid())
			return;
			
		if (_asyncHandlerForAddressables.PercentComplete.Equals(1f))
		{
			StopCoroutine(FadeInRoutine());
			StartCoroutine(FadeOutRoutine());
		}

		_percentText.text = _asyncHandlerForAddressables.PercentComplete.ToString("P0");
	}
	
	private void SwitchLoadingText()
	{
		if (!_loadingAnimationCD.isReady)
			return;
		
		var stringDots = "";
		
		for (var i = 0; i < _dotsCount; i++)
			stringDots += ".";
		
		if (_dotsCount == 3)
			_dotsCount = 0;
		else
			_dotsCount++;

		_loadingText.text = "Loading" + stringDots;
		_loadingAnimationCD.Reset();
	}
	
	private IEnumerator FadeInRoutine()
	{
		_canvasGroup.blocksRaycasts = true;
		
		if (_canvasGroup.alpha < 1)
			_canvasGroup.alpha += Time.deltaTime;
		
		yield return null;
		
		if (_canvasGroup.alpha.Equals(1))
			StopCoroutine(FadeInRoutine());
		else
			StartCoroutine(FadeInRoutine());
	}
	
	private IEnumerator FadeOutRoutine()
	{
		if (_canvasGroup.alpha > 0)
			_canvasGroup.alpha -= Time.deltaTime;
		
		yield return null;

		if (_canvasGroup.alpha.Equals(0))
		{
			StopCoroutine(FadeOutRoutine());
			_canvasGroup.blocksRaycasts = false;
		}
		else
			StartCoroutine(FadeOutRoutine());
	}

	private void SetDefaultCanvasGroupSettings()
	{
		_canvasGroup.alpha = 0f;
		_canvasGroup.blocksRaycasts = false;
	}
	
	private void IsAppConnectedToInternet()
	{
		if (!_internetCheckCD.isReady)
			return;

		StartCoroutine(OnlineCheckRoutine());
		_internetLogo.color = _isOnline ? Color.green : Color.red;
		
		if (IsFirstTimeOpened() && !_isOnline)
			_internetWarning.gameObject.SetActive(true);
		else
			_internetWarning.gameObject.SetActive(false);

		_internetCheckCD.Reset();
		
		
		IEnumerator OnlineCheckRoutine()
		{
			var googleRequest = UnityWebRequest.Get("http://google.com");
			yield return googleRequest.SendWebRequest();
			_isOnline = googleRequest.error == null;
		}
	}
	
	private bool IsFirstTimeOpened()
	{
		return PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1;
	}

	private void SetFirstLaunchToFalse(Scene scene, LoadSceneMode loadMode)
	{
		if (scene.name == "Game")
			PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
	}
	
	private void OnDisable()
	{
		SceneManager.sceneLoaded -= AudioManager.Instance.ChangeMusic;
		SceneManager.sceneLoaded -= SetFirstLaunchToFalse;
	}
}


