using Boilerplate.EventChannels;
using TowerDefence.TowersCommons;
using UnityEngine;

namespace TowerDefence.EnemiesCommons
{
    [CreateAssetMenu(fileName = "new TowerTypeEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "TowerType")]
    public class TowerTypeEventChannel : EventChannel<TowerType>
    {
    }
}