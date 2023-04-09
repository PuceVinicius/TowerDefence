using UnityEngine;

namespace Boilerplate.FuncChannels
{
    [CreateAssetMenu(fileName = "new CameraVoidFuncChannel", menuName = FuncUtils.FUNC_ASSET_MENU_BASE_NAME_PREFIX + "Camera Void")]
    public class CameraVoidFuncChannel : FuncChannel<Camera>
    {
    }
}