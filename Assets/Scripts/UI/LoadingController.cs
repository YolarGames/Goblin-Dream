using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class LoadingController : MonoBehaviourSingleton<LoadingController>
{
	[SerializeField] private Text _loadingText;
	[SerializeField] private Text _percentText;
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private Cooldown _loadingAnimationCD;
	private AsyncOperationHandle<SceneInstance> _asyncHandlerForAddressables;
	private AsyncOperation _asyncOperation;
	private int _dotsCount;

	public void LoadGameScene()
	{
		StartCoroutine(FadeInRoutine());
		_asyncHandlerForAddressables = Addressables.LoadSceneAsync("Game", LoadSceneMode.Single); ;
	}
	
	public void LoadMainMenuScene()
	{
		StartCoroutine(FadeInRoutine());
		_asyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
	}
	
	private void Start()
	{
		_loadingAnimationCD.Reset();
		SetDefaultCanvasGroupSettings();
	}

	private void LateUpdate()
	{
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

}
