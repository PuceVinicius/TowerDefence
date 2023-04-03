using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new BoolEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Bool")]
    public class BoolEventChannel : EventChannel<bool>
    {
    }
}