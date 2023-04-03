using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.Video;

namespace Boilerplate.SceneManagement
{
    public class Onboarding : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private VideoPlayer _introVideoPlayer;
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Broadcasters")]
        [SerializeField] private GameSceneEventChannel _loadSceneEventChannel;

        #endregion Variables

        #region Messages

        private void Awake()
        {
            if (!_introVideoPlayer.clip)
            {
                VideoEnded();
                return;
            }

            _introVideoPlayer.loopPointReached += VideoEnded;
            _introVideoPlayer.Play();
        }

        private void OnDestroy()
        {
            _introVideoPlayer.loopPointReached -= VideoEnded;
        }

        #endregion

        #region Methods

        private void VideoEnded(VideoPlayer source = null)
        {
            EventUtils.BroadcastEvent(_loadSceneEventChannel, _genericScenes.MenuScene);
        }

        #endregion
    }
}