using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class StaffCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Staff;
        public LocaleString Name => LocaleString.Staff;
        public string IconResref => "skl_staff";
        public FeatType LoreFeat => FeatType.StaffLore;
        public FeatType PassiveFeat => FeatType.Shattersoul;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.QuarterStaff,
        ];
    }
}
