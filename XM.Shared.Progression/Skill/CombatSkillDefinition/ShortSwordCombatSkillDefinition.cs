using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class ShortSwordCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.ShortSword;
        public LocaleString Name => LocaleString.ShortSword;
        public string IconResref => "skl_shortsword";
        public FeatType LoreFeat => FeatType.ShortSwordLore;
        public FeatType PassiveFeat => FeatType.FrostbiteBlade;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.ShortSword,
        ];
    }
}
