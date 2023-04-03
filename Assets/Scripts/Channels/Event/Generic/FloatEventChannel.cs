using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new FloatEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Float")]
    public class FloatEventChannel : EventChannel<float>
    {
    }
}