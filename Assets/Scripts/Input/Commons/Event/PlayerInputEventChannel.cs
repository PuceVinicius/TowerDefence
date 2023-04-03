using UnityEngine;
using UnityEngine.InputSystem;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new PlayerInputEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "PlayerInput")]
    public class PlayerInputEventChannel : EventChannel<PlayerInput>
    {
    }
}