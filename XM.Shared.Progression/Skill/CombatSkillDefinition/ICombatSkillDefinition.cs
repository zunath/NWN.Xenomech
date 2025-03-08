using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    public interface ICombatSkillDefinition
    {
        SkillType Type { get; }
        LocaleString Name { get; }
        string IconResref { get; }
        FeatType LoreFeat { get; }
        FeatType PassiveFeat { get; }
        List<BaseItemType> BaseItems { get; }
    }
}
