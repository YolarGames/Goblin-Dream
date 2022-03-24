using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviourSingleton<LoadingController>
{
	[SerializeField] private Text _loadingText;
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private Cooldown _loadingAnimationCD;
	private int _dotsCount;
	
	private void Start()
	{
		_loadingAnimationCD.Reset();
	}

	private void LateUpdate()
	{
		SwitchLoadingText();
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
	
	private IEnumerator FadeOutRoutine()
	{
		if (_canvasGroup.alpha > 0)
			_canvasGroup.alpha -= Time.deltaTime / 2;
		
		yield return null;

		if (_canvasGroup.alpha.Equals(0))
		{
			StopCoroutine(FadeOutRoutine());
			gameObject.SetActive(false);
		}
		else
			StartCoroutine(FadeOutRoutine());
	}
	
	private IEnumerator FadeInRoutine()
	{
		gameObject.SetActive(true);
		
		if (_canvasGroup.alpha < 1)
			_canvasGroup.alpha += Time.deltaTime / 2;
		
		yield return null;
		
		if (_canvasGroup.alpha.Equals(1))
			StopCoroutine(FadeInRoutine());
		else
			StartCoroutine(FadeInRoutine());
	}

}
