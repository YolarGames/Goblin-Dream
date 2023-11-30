using GameCore.GameServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore
{
	public sealed class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private int _frameRate;
		private Game _game;
		
		private void Awake() =>
			_game = new Game();
	}
}

public class Game
{
	private GameServices _services = new();
	private const string FIRST_LAUNCH_PREFS_INT = "first_launch";
	
	public Game() =>
		SceneManager.sceneLoaded += SetFirstLaunch;

	public static void Quit()
	{
		#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
		#else
		Application.Quit();
		#endif
	}

	public static void SetPause(bool value) =>
		Time.timeScale = value ? 0 : 1;
	
	public static bool IsFirstLaunch() =>
		PlayerPrefs.GetInt(FIRST_LAUNCH_PREFS_INT, 1) == 1;

	private void SetFirstLaunch(Scene scene, LoadSceneMode loadMode)
	{
		if (scene.name == "game")
			PlayerPrefs.SetInt(FIRST_LAUNCH_PREFS_INT, 0);
	}
}