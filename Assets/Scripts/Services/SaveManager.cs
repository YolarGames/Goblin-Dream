using System.IO;
using UnityEngine;

namespace Services
{
	public static class SaveManager
	{
		private static readonly string _savePath = Path.Combine(Application.persistentDataPath, "save.json");
	
		public static void SaveDataToJson(string dataPath, string buildVersion)
		{
			if (File.Exists(_savePath))
				File.Delete(_savePath);
		
			var saveData = new SaveData(dataPath, buildVersion);
			var jsonString = JsonUtility.ToJson(saveData);
			File.WriteAllText(_savePath, jsonString);

			var some = File.ReadAllText(_savePath);
			Debug.Log(some);
		}

		public static void LoadFromJson(SaveData saveData)
		{
			if (!File.Exists(_savePath))
				Debug.LogError("Can't find save file!");
		}
	}

	public class SaveData
	{
		public string DataPath;
		public string BuildVersion;

		public SaveData(string path, string version)
		{
			DataPath = path;
			BuildVersion = version;
		}
	}
}