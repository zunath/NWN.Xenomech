using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class LongswordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Longsword;
        public LocaleString Name => LocaleString.Longsword;
        public string IconResref => "skl_longsword";
        public FeatType LoreFeat => FeatType.LongswordLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.LongSword,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.FastBlade},
            {160, FeatType.BurningBlade},
            {240, FeatType.RedLotusBlade},
            {320, FeatType.FlatBlade},
            {540, FeatType.ShiningBlade},
            {860, FeatType.SeraphBlade},
            {1130, FeatType.VorpalBlade},
            {1390, FeatType.SwiftBlade},
            {1430, FeatType.SanguineBlade},
            {1500, FeatType.Atonement},
        };
    }
}
