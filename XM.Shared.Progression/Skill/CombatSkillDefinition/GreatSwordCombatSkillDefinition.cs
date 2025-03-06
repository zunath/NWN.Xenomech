using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class GreatSwordCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.GreatSword;
        public LocaleString Name => LocaleString.GreatSword;
        public string IconResref => "skl_greatsword";
        public FeatType LoreFeat => FeatType.GreatSwordLore;
        public FeatType PassiveFeat => FeatType.Scourge;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.GreatSword
        ];
    }
}
