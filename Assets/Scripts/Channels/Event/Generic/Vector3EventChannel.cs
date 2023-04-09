using UnityEngine;

namespace Boilerplate.EventChannels
{
    [CreateAssetMenu(fileName = "new Vector3EventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Vector3")]
    public class Vector3EventChannel : EventChannel<Vector3>
    {
    }
}