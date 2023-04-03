using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new IntEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Int")]
    public class IntEventChannel : EventChannel<int>
    {
    }
}