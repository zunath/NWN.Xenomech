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
            {160, FeatType.HeavySwing},
            {240, FeatType.RockCrusher},
            {540, FeatType.EarthCrusher},
            {860, FeatType.Starburst},
            {1130, FeatType.Omniscience},
            {1390, FeatType.SpiritTaker},
            {1430, FeatType.Shattersoul},
        };
    }
}
