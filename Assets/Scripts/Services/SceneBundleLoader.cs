using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Services
{
	public class SceneBundleLoader : MonoBehaviour
	{
		[SerializeField] private string _scene;
		[SerializeField] private string _mainMenu;
		private AsyncOperationHandle<SceneInstance> _gameLoadAsyncOperation;
		private AsyncOperation _mainMenuLoadAsync;
		private LoadingScreen _loadingScreen;

		private void Awake() =>
			_loadingScreen = GetComponent<LoadingScreen>();

		private void LateUpdate()
		{
			UpdateSceneLoadStatus();
			UpdateSceneLoadStatusForAddressables();
		}

		public void StartMainMenuLoading() =>
			_loadingScreen.Show(LoadMainMenu);

		public void StartSceneLoading() =>
			_loadingScreen.Show(LoadScene);
		
		private void LoadMainMenu()
		{
			_mainMenuLoadAsync = SceneManager.LoadSceneAsync(_mainMenu, LoadSceneMode.Single);
			_mainMenuLoadAsync.completed += _loadingScreen.Hide;
		}

		private void LoadScene()
		{
			_gameLoadAsyncOperation = Addressables.LoadSceneAsync(_scene);
			_gameLoadAsyncOperation.Completed += _loadingScreen.Hide;
		}

		private void UpdateSceneLoadStatusForAddressables()
		{
			if (!_gameLoadAsyncOperation.IsValid())
				return;

			string percentText = _gameLoadAsyncOperation.PercentComplete.ToString("P0");
			_loadingScreen.SetCompleteStatus(percentText);
		}

		private void UpdateSceneLoadStatus()
		{
			if (_mainMenuLoadAsync == null)
				return;

			string percentText = _mainMenuLoadAsync.progress.ToString("P0");
			_loadingScreen.SetCompleteStatus(percentText);
		}
	}
}