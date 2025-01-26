using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class StaffSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Staff;
        public LocaleString Name => LocaleString.Staff;
        public string IconResref => "skl_staff";
        public FeatType LoreFeat => FeatType.StaffLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.QuarterStaff,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.HeavySwing},
            {160, FeatType.RockCrusher},
            {240, FeatType.EarthCrusher},
            {320, FeatType.Starburst},
            {540, FeatType.Sunburst},
            {860, FeatType.SpiritTaker},
            {1130, FeatType.Retribution},
            {1390, FeatType.Omniscience},
            {1430, FeatType.Cataclysm},
            {1500, FeatType.Shattersoul},
        };
    }
}
