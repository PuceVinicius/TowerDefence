using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new StringEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "String")]
    public class StringEventChannel : EventChannel<string>
    {
        
    }
}