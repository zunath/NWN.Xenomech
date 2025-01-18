using XM.Shared.Core.Localization;

namespace XM.Progression.Recast
{
    // Note: Short names are what's displayed on the recast Gui element. They are limited to 14 characters.
    public enum RecastGroup
    {
        [RecastGroup(LocaleString.Invalid, LocaleString.Invalid, false)]
        Invalid = 0,
        [RecastGroup(LocaleString.EtherBloom, LocaleString.EtherBloom, true)]
        EtherBloom = 1,
    }
}
