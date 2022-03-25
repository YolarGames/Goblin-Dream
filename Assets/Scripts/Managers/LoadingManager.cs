using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using File = UnityEngine.Windows.File;

public class LoadingManager : MonoBehaviourSingleton<LoadingManager>
{
	[SerializeField] private string _url;
	[SerializeField] private string _name;
	[SerializeField] private uint _version;
	[SerializeField] private uint _crc;
	private const string FileType = ".unity3d";

	private void Start()
	{
		StartCoroutine(FirstLaunchLoading());
	}

	private IEnumerator FirstLaunchLoading()
	{
		var path = Path.Combine(Application.persistentDataPath, _name + FileType);
		Debug.Log("PATH: " + path);
		
		
		using var webRequest = UnityWebRequestAssetBundle.GetAssetBundle(_url, _version, _crc);
		yield return webRequest.SendWebRequest();
		Debug.Log("Web request is done");

		if (webRequest.result != UnityWebRequest.Result.Success)
		{
			Debug.Log(webRequest.error);
			yield break;
		}
		
		var assetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
	}

	private void SerializeBundle(AssetBundle bundle, string path)
	{
		var binaryFormatter = GetBinaryFormatter();
		var fileStream = new FileStream(path, FileMode.Create);
		
		binaryFormatter.Serialize(fileStream, bundle);
		fileStream.Close();
	}

	private AssetBundle DeserializeBundle(string path)
	{
		if (!File.Exists(path))
			return null;
		
		var binaryFormatter = GetBinaryFormatter();
		var fileStream = new FileStream(path, FileMode.Open);

		try
		{
			var bundle = binaryFormatter.Deserialize(fileStream) as AssetBundle;
			fileStream.Close();
			return bundle;
		}
		catch (Exception exception)
		{
			Debug.Log(exception.Message);
		}

		return null;
	}

	private BinaryFormatter GetBinaryFormatter()
	{
		var binaryFormatter = new BinaryFormatter();
		// var surrogateSelector = new SurrogateSelector();
		// var bundleSurrogate = new AssetBundleSurrogate();
		
		// surrogateSelector.AddSurrogate(
		// 	type:typeof(AssetBundleSurrogate),
		// 	context:new StreamingContext(StreamingContextStates.All),
		// 	surrogate:bundleSurrogate);
		
		// binaryFormatter.SurrogateSelector = surrogateSelector;
		return binaryFormatter;
	}
}