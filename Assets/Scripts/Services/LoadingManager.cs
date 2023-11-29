using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Services
{
	public class LoadingManager : MonoBehaviourSingleton<LoadingManager>
	{
		[SerializeField] private Text _loadingText;
		[SerializeField] private Text _percentText;
		[SerializeField] private Text _internetWarning;
		[SerializeField] private CanvasGroup _canvasGroup;
		[SerializeField] private Cooldown _loadingAnimationCd;
		[SerializeField] private Cooldown _internetCheckCd;
		[SerializeField] private Image _internetLogo;

		private AsyncOperationHandle<SceneInstance> _gameAsyncOperationHandle;
		private AsyncOperation _mainMenuLoadAsync;
		private int _dotsCount;
		private bool _isOnline;

		public void LoadGameScene()
		{
			if (IsFirstTimeOpened() && !_isOnline)
				return;
		
			StartCoroutine(FadeInRoutine());
			_gameAsyncOperationHandle = Addressables.LoadSceneAsync("Game", LoadSceneMode.Single);
			SaveManager.SaveDataToJson(Application.persistentDataPath, Application.version);
		}

		public void LoadMainMenuScene()
		{
			_mainMenuLoadAsync = SceneManager.LoadSceneAsync("mainMenu", LoadSceneMode.Single);
		}
	
		private void Start()
		{
			_loadingAnimationCd.Reset();
			_internetCheckCd.Reset();
			SetDefaultCanvasGroupSettings();
		
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
			if (_mainMenuLoadAsync == null)
				return;
			
			if (_mainMenuLoadAsync.progress.Equals(1f))
			{
				StopCoroutine(FadeInRoutine());
				StartCoroutine(FadeOutRoutine());
			}

			_percentText.text = _mainMenuLoadAsync.progress.ToString("P0");
		}
	
		private void UpdateSceneLoadStatusForAddressables()
		{
			if (!_gameAsyncOperationHandle.IsValid())
				return;
			
			if (_gameAsyncOperationHandle.PercentComplete.Equals(1f))
			{
				StopCoroutine(FadeInRoutine());
				StartCoroutine(FadeOutRoutine());
			}

			_percentText.text = _gameAsyncOperationHandle.PercentComplete.ToString("P0");
		}
	
		private void SwitchLoadingText()
		{
			if (!_loadingAnimationCd.IsReady)
				return;
		
			var stringDots = "";
		
			for (var i = 0; i < _dotsCount; i++)
				stringDots += ".";
		
			if (_dotsCount == 3)
				_dotsCount = 0;
			else
				_dotsCount++;

			_loadingText.text = "Loading" + stringDots;
			_loadingAnimationCd.Reset();
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
			if (!_internetCheckCd.IsReady)
				return;

			Debug.LogWarning(_isOnline);
			
			StartCoroutine(OnlineCheckRoutine());
			_internetLogo.color = _isOnline ? Color.green : Color.red;
		
			if (IsFirstTimeOpened() && !_isOnline)
				_internetWarning.gameObject.SetActive(true);
			else
				_internetWarning.gameObject.SetActive(false);

			_internetCheckCd.Reset();
		
			IEnumerator OnlineCheckRoutine()
			{
				var googleRequest = UnityWebRequest.Get("http://google.com");
				yield return googleRequest.SendWebRequest();
				_isOnline = googleRequest.error == null;
			}
		}
	
		private bool IsFirstTimeOpened() =>
			PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1;

		private void SetFirstLaunchToFalse(Scene scene, LoadSceneMode loadMode)
		{
			if (scene.name == "game")
				PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
		}
	
		private void OnDisable() =>
			SceneManager.sceneLoaded -= SetFirstLaunchToFalse;
	}
}


