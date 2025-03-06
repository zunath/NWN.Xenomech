using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class DaggerCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Dagger;
        public LocaleString Name => LocaleString.Dagger;
        public string IconResref => "skl_dagger";
        public FeatType LoreFeat => FeatType.DaggerLore;
        public FeatType PassiveFeat => FeatType.DancingEdge;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Dagger
        ];
    }
}
