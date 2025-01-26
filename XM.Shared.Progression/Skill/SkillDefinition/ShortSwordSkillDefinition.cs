using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class ShortSwordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.ShortSword;
        public LocaleString Name => LocaleString.ShortSword;
        public string IconResref => "skl_shortsword";
        public FeatType LoreFeat => FeatType.ShortSwordLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.ShortSword,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.PiercingBlade},
            {160, FeatType.BurningEdge},
            {240, FeatType.ShadowStrike},
            {320, FeatType.IceFang},
            {540, FeatType.SonicSlash},
            {860, FeatType.EmberFang},
            {1130, FeatType.Lightfang},
            {1390, FeatType.FrostbiteBlade},
            {1430, FeatType.ThunderSlash},
            {1500, FeatType.SoulBlade},
        };
    }
}
