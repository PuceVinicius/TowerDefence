using UnityEngine;

namespace Boilerplate.FuncChannels
{
    [CreateAssetMenu(fileName = "new StringFloatFuncChannel", menuName = FuncUtils.FUNC_ASSET_MENU_BASE_NAME_PREFIX + "Float String")]
    public class FloatStringFuncChannel : FuncChannel<float, string>
    {
    }
}