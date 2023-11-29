using GameCore.Services;
using UnityEditor;
using UnityEngine;

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
}