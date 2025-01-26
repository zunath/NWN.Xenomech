using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class PolearmSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Polearm;
        public LocaleString Name => LocaleString.Polearm;
        public string IconResref => "skl_polearm";
        public FeatType LoreFeat => FeatType.PolearmLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.ShortSpear,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.DoubleThrust},
            {160, FeatType.ThunderThrust},
            {240, FeatType.RaidenThrust},
            {320, FeatType.PentaThrust},
            {540, FeatType.VorpalThrust},
            {860, FeatType.SonicThrust},
            {1130, FeatType.Drakesbane},
            {1390, FeatType.WheelingThrust},
            {1430, FeatType.Stardiver},
            {1500, FeatType.Skewer},
        };
    }
}
