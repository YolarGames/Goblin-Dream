using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingManager : MonoBehaviour
{
	[SerializeField] private string _companyName;
	[SerializeField] private string _gameName;
	[SerializeField] private string _fileType;
	[SerializeField] private uint _version;
	[SerializeField] private uint _crc;
	[SerializeField] private BundlePath[] _bundles;


	private IEnumerator FirstLaunchLoading()
	{
		var bundle = _bundles[0];
		var path = Path.Combine(
			Application.persistentDataPath,
			_companyName,
			_gameName,
			bundle.bundleName + _fileType);

		#region Download bundle
		using var webRequest = UnityWebRequestAssetBundle.GetAssetBundle(bundle.url, _version, _crc);
		yield return webRequest.SendWebRequest();

		if (webRequest.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(webRequest.error);
			yield break;
		}
		#endregion

		#region Save bundle
		var handler = webRequest.downloadHandler;
		File.WriteAllBytes(path, handler.data);
		#endregion
		
		#region Load bundle from device

		#endregion

	}
}