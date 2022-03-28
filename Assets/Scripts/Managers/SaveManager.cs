using UnityEngine;
using System.IO;

public static class SaveManager
{
	private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "save.json");
	
	
	public static void SaveDataToJson(string dataPath, string buildVersion)
	{
		if (File.Exists(SavePath))
			File.Delete(SavePath);
		
		var saveData = new SaveData(dataPath, buildVersion);
		var jsonString = JsonUtility.ToJson(saveData);
		File.WriteAllText(SavePath, jsonString);

		var some = File.ReadAllText(SavePath);
		Debug.Log(some);
	}

	public static void LoadFromJson(SaveData saveData)
	{
		if (!File.Exists(SavePath))
			Debug.LogError("Can't find save file!");
	}
}

public class SaveData
{
	public string dataPath;
	public string buildVersion;

	public SaveData(string path, string version)
	{
		dataPath = path;
		buildVersion = version;
	}
}