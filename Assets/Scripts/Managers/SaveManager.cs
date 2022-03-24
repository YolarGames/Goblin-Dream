using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using UnityEngine;

public static class SaveManager
{
	private const string FileName = "bundle.some";
	private static string _path;


	public static void SaveBundle(AssetBundle assetBundle)
	{
		_path = Path.Combine(Application.persistentDataPath, "data", FileName); // Set up file path
		var formatter = new BinaryFormatter(); // Create a BimaryFormatter
		var stream = new FileStream(_path, FileMode.Create); // Create a FileStream
		
		formatter.Serialize(stream, assetBundle); // Call formatter.Serialize
		stream.Close(); // Close the stream
	}

	public static void LoadBundle()
	{
		if (File.Exists(_path))
		{
			
			var formatter = new BinaryFormatter();
			var stream = new FileStream(_path, FileMode.Open);
			
			// TODO: Check if File not corrupted
			// if (stream.Equals())
			// {
			// 	
			// }

			
			formatter.Deserialize(stream);
			stream.Close();
		}
		else // If !Exists try load data from server
		{
			
		}
	}

	private static void DownloadFile()
	{
		
	}
}