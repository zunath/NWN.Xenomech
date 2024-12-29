namespace XM.API.Constants
{
    public enum SpellTargetingFlagsType
    {
        None = 0,
        HarmsEnemies = 1,
        HarmsAllies = 2,
        HelpsAllies = 4,
        IgnoresSelf = 8,
        OriginOnSelf = 16,
        SuppressWithTarget = 32
    }
}