using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class GreatAxeCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.GreatAxe;
        public LocaleString Name => LocaleString.GreatAxe;
        public string IconResref => "skl_greataxe";
        public FeatType LoreFeat => FeatType.GreatAxeLore;
        public FeatType PassiveFeat => FeatType.Upheaval;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.GreatAxe
        ];
    }
}
