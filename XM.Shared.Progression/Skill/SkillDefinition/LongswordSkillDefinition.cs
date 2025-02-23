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
            BaseItemType.Longsword,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.FastBlade},
            {240, FeatType.BurningBlade},
            {540, FeatType.RedLotusBlade},
            {860, FeatType.VorpalBlade},
            {1130, FeatType.FlatBlade},
            {1390, FeatType.Atonement},
            {1430, FeatType.ShiningBlade},
        };
    }
}
