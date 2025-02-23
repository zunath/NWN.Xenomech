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
            {160, FeatType.PiercingBlade},
            {240, FeatType.BurningEdge},
            {540, FeatType.SoulBlade},
            {860, FeatType.IceFang},
            {1130, FeatType.SonicSlash},
            {1390, FeatType.EmberFang},
            {1430, FeatType.FrostbiteBlade},
        };
    }
}
