using System;
using System.Collections;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Boilerplate.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Listeners")]
        [SerializeField] private GameSceneEventChannel _loadSceneEvent;

        [Foldout("Broadcasters")]
        [SerializeField] private GameSceneEventChannel _onSceneLoadedEvent;
        [SerializeField] private BoolEventChannel _toggleLoadingScreenEvent;

        private GameScene _currentSceneLoaded = null;

        private bool _isLoading = false;
        private string _coldStartupSceneName = "";

        #endregion Variables

        #region Messages

        private void OnEnable()
        {
            EventUtils.AddEventListener(_loadSceneEvent, LoadScene);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_loadSceneEvent, LoadScene);
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (SceneManagementStatics.IsColdStartup)
                GetColdStartupScene();
#endif
        }

        #endregion Messages

        #region Methods

        private void LoadScene(GameScene nextScene)
        {
            if (_isLoading) return;

            StartCoroutine(LoadSceneFlow(nextScene));
        }

        private IEnumerator LoadSceneFlow(GameScene nextScene)
        {
            SetupUnload(nextScene);

            yield return StartCoroutine(UnloadPreviousScene());

            yield return StartCoroutine(LoadNextScene(nextScene));

            OnLoadEnd(nextScene);
        }

        private void SetupUnload(GameScene nextScene)
        {
            _isLoading = true;
            if (nextScene.HasLoadingScreen)
                EventUtils.BroadcastEvent(_toggleLoadingScreenEvent, true);
        }

        private void OnLoadEnd(GameScene nextScene)
        {
            _isLoading = false;
            if (nextScene.HasLoadingScreen)
                EventUtils.BroadcastEvent(_toggleLoadingScreenEvent, false);
            EventUtils.BroadcastEvent(_onSceneLoadedEvent, _currentSceneLoaded);
        }

        private IEnumerator UnloadPreviousScene()
        {
#if UNITY_EDITOR
            UnloadColdStartupScene();
#endif

            if (!_currentSceneLoaded
                || !_currentSceneLoaded.SceneReference.OperationHandle.IsValid()
                || _currentSceneLoaded.IsPermanent)
                yield break;

            var asyncUnload = _currentSceneLoaded.SceneReference.UnLoadScene();

            yield return asyncUnload;
        }

        private IEnumerator LoadNextScene(GameScene nextScene)
        {
            var asyncLoad = nextScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

            yield return asyncLoad;

            if (asyncLoad.Status != AsyncOperationStatus.Succeeded)
                yield break;

            SceneManager.SetActiveScene(asyncLoad.Result.Scene);
            _currentSceneLoaded = nextScene;
        }
#if UNITY_EDITOR

        private void GetColdStartupScene()
        {
            _coldStartupSceneName = SceneManager.GetActiveScene().name;
        }

        private void UnloadColdStartupScene()
        {
            if (!SceneManagementStatics.IsColdStartup)
                return;

            if (_coldStartupSceneName == "")
                return;

            SceneManager.UnloadSceneAsync(_coldStartupSceneName);
            _coldStartupSceneName = "";
        }

#endif

        #endregion Methods
    }
}