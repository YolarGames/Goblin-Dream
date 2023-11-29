using UnityEngine.SceneManagement;

namespace Utils
{
	public static class Scenes
	{
		private const string SCENE_PATH = "Assets/Scenes/";
		private const string SCENE_FORMAT = ".unity";
		public const string MAIN_MENU = "mainMenu";
		public const string GAME = "game";
		
		public static string ActiveSceneName { get; } = SceneManager.GetActiveScene().name;

		public static string GetScenePath(string sceneName) =>
			SCENE_PATH + sceneName + SCENE_FORMAT;
	}
}