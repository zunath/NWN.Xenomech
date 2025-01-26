using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class BowSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Bow;
        public LocaleString Name => LocaleString.Bow;
        public string IconResref => "skl_bow";
        public FeatType LoreFeat => FeatType.BowLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.LongBow,
            BaseItemType.ShortBow
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.FlamingArrow},
            {160, FeatType.PiercingArrow},
            {240, FeatType.DullingArrow},
            {320, FeatType.Sidewinder},
            {540, FeatType.BlastArrow},
            {860, FeatType.ArchingArrow},
            {1130, FeatType.EmpyrealArrow},
            {1390, FeatType.NamasArrow},
            {1430, FeatType.ApexArrow},
            {1500, FeatType.RadiantArrow},
        };
    }
}
