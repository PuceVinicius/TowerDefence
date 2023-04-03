using UnityEngine;

namespace Boilerplate.FuncChannels
{
    [CreateAssetMenu(fileName = "new IntVoidFuncChannel", menuName = FuncUtils.FUNC_ASSET_MENU_BASE_NAME_PREFIX + "Int Void")]
    public class IntVoidFuncChannel : FuncChannel<int>
    {
    }
}