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
        public FeatType PassiveFeat => FeatType.Shattersoul;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.QuarterStaff,
        ];
    }
}
