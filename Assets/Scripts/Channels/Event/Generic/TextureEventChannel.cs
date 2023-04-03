using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new TextureEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Texture")]
    public class TextureEventChannel : EventChannel<Texture>
    {
    }
}