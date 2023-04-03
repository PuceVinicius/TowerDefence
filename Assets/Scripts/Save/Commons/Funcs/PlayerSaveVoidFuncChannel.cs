using Boilerplate.SaveCommons;
using UnityEngine;

namespace Boilerplate.FuncChannels
{
    [CreateAssetMenu(fileName = "new PlayerSaveVoidFuncChannel", menuName = FuncUtils.FUNC_ASSET_MENU_BASE_NAME_PREFIX + "Bool Void")]
    public class PlayerSaveVoidFuncChannel : FuncChannel<PlayerSave>
    {
    }
}