using UnityEngine;

namespace Boilerplate.FuncChannels
{
    [CreateAssetMenu(fileName = "new BoolVoidFuncChannel", menuName = FuncUtils.FUNC_ASSET_MENU_BASE_NAME_PREFIX + "Bool Void")]
    public class BoolVoidFuncChannel : FuncChannel<bool>
    {
    }
}