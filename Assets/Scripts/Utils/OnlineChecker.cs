using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utils
{
	public sealed class OnlineChecker : MonoBehaviour
	{
		[SerializeField] private Cooldown _checkInterval;
		[SerializeField] private TMP_Text _firstLaunchText;
		[SerializeField] private Image _internetLogo;
		private bool _isOnline;

		private void LateUpdate()
		{
			IsAppConnectedToInternet();
			CheckFirstLaunch();
		}

		private void CheckFirstLaunch()
		{
			if(Game.IsFirstLaunch() && !_isOnline)
				ShowInternetWarning();
			else
				HideInternetWarning();
		}

		private void ShowInternetWarning() =>
			_firstLaunchText.gameObject.SetActive(true);

		private void HideInternetWarning() =>
			_firstLaunchText.gameObject.SetActive(false);

		private void IsAppConnectedToInternet()
		{
			if (!_checkInterval.IsReady)
				return;

			StartCoroutine(OnlineCheckRoutine());
			_internetLogo.color = _isOnline ? Color.green : Color.red;

			_checkInterval.Reset();
		}

		private IEnumerator OnlineCheckRoutine()
		{
			var request = UnityWebRequest.Get("http://youtube.com");
			yield return request.SendWebRequest();
			_isOnline = request.error == null;
		}
	}
}