using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoot : MonoBehaviour
{
	[SerializeField] private SceneAsset _sceneAsset;
	[SerializeField] private AudioClip _sceneClip;

	private void Start()
	{
		string currentScene = SceneManager.GetActiveScene().name;
		
		if (SceneManager.GetActiveScene().name != currentScene)
		{
			EditorSceneManager.playModeStartScene = _sceneAsset;
			SceneManager.LoadScene(currentScene);
		}
		AudioManager.Instance.SetMusic(_sceneClip);
	}
}