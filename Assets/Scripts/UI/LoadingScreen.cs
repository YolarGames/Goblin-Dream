using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Utils;

namespace UI
{
	public sealed class LoadingScreen : MonoBehaviour
	{
		[SerializeField] private Cooldown _textUpdateInterval;
		[SerializeField] private TMP_Text _loadingText;
		[SerializeField] private TMP_Text _percentText;
		[SerializeField] private ShowHideCanvasGroup _canvasGroup;
		private int _dotsCount;

		private void LateUpdate() =>
			UpdateLoadingText();

		public void Show(UnityAction onShown = null)
		{
			if (onShown != null)
				_canvasGroup.OnShown.AddListener(onShown);
			_canvasGroup.Show();
		}

		public void Hide(AsyncOperationHandle<SceneInstance> obj) =>
			_canvasGroup.Hide();

		public void Hide(AsyncOperation obj) =>
			_canvasGroup.Hide();

		public void SetCompleteStatus(string percent) =>
			_percentText.SetText(percent);

		private void UpdateLoadingText()
		{
			if (!_textUpdateInterval.IsReady)
				return;

			_loadingText.text = "Loading" + CountDots();
			_textUpdateInterval.Reset();
		}

		private string CountDots()
		{
			string stringDots = "";

			for (int i = 0; i < _dotsCount; i++)
				stringDots += ".";

			if (_dotsCount == 3)
				_dotsCount = 0;
			else
				_dotsCount++;
			return stringDots;
		}
	}
}