namespace TowerDefence.TowersCommons
{
    public static class TowersConsts
    {
        public const string TOWERS_ASSET_MENU_NAME_PREFIX = "ScriptableObjects/Units/Towers/";
    }

    public enum TowerType
    {
        None,
        Fast,
        Burst,
        Area
    }

    public enum TargetType
    {
        None,
        Closer,
        Area,
        Tougher
    }
}