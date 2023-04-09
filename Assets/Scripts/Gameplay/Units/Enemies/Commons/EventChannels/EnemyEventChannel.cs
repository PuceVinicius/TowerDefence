using Boilerplate.EventChannels;
using UnityEngine;

namespace TowerDefence.EnemiesCommons
{
    [CreateAssetMenu(fileName = "new EnemyEventChannel", menuName = EventUtils.EVENT_ASSET_MENU_BASE_NAME_PREFIX + "Enemy")]
    public class EnemyEventChannel : EventChannel<Enemy>
    {
    }
}