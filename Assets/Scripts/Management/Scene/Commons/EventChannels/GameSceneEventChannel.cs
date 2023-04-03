using Boilerplate.EventChannels;
using UnityEngine;

namespace Boilerplate.SceneManagement
{
    [CreateAssetMenu(fileName = "New GameSceneEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "GameScene")]
    public class GameSceneEventChannel : EventChannel<GameScene>
    {
    }
}