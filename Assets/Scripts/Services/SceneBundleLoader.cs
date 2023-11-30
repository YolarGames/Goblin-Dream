using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Utils;

namespace Services
{
	public class SceneBundleLoader : MonoBehaviourSingleton<SceneBundleLoader>
	{
		[SerializeField] private string _scene;
		[SerializeField] private string _mainMenu;
		private AsyncOperationHandle<SceneInstance> _gameLoadAsyncOperation;
		private AsyncOperation _mainMenuLoadAsync;
		private LoadingScreen _loadingScreen;

		public override void Awake()
		{
			base.Awake();
			_loadingScreen = GetComponent<LoadingScreen>();
		}

		private void LateUpdate()
		{
			UpdateSceneLoadStatus();
			UpdateSceneLoadStatusForAddressables();
		}

		public void StartMainMenuLoading() =>
			_loadingScreen.Show(LoadMainMenu);

		public void StartSceneLoading() =>
			_loadingScreen.Show(LoadScene);
		
		private void LoadMainMenu() =>
			_mainMenuLoadAsync = SceneManager.LoadSceneAsync(_mainMenu, LoadSceneMode.Single);

		private void LoadScene() =>
			_gameLoadAsyncOperation = Addressables.LoadSceneAsync(_scene);

		private void UpdateSceneLoadStatusForAddressables()
		{
			if (!_gameLoadAsyncOperation.IsValid())
				return;

			if (_gameLoadAsyncOperation.PercentComplete.Equals(1f))
				_loadingScreen.Hide(null);

			string percentText = _gameLoadAsyncOperation.PercentComplete.ToString("P0");
			_loadingScreen.SetCompleteStatus(percentText);
		}

		private void UpdateSceneLoadStatus()
		{
			if (_mainMenuLoadAsync == null)
				return;

			if (_mainMenuLoadAsync.progress.Equals(1f))
				_loadingScreen.Hide(null);

			string percentText = _mainMenuLoadAsync.progress.ToString("P0");
			_loadingScreen.SetCompleteStatus(percentText);
		}
	}
}