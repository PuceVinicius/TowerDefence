using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Boilerplate.SceneManagement
{
    public class Bootstrap : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Broadcasters")]
        [SerializeField] private AssetReference _loadSceneChannel;

        #endregion

        #region Messages

        private void Start()
        {
            SceneManagementStatics.IsColdStartup = false;

            _genericScenes.ManagersScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadEventChannel;
        }

        #endregion

        #region Methods

        private void LoadEventChannel(AsyncOperationHandle<SceneInstance> obj)
        {
            _loadSceneChannel.LoadAssetAsync<GameSceneEventChannel>().Completed += LoadFirstScene;
        }

        private void LoadFirstScene(AsyncOperationHandle<GameSceneEventChannel> obj)
        {
            EventUtils.BroadcastEvent(obj.Result, _genericScenes.OnboardingScene);

            SceneManager.UnloadSceneAsync(0);
        }

        #endregion
    }
}