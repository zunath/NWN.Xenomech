using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class HandToHandCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.HandToHand;
        public LocaleString Name => LocaleString.HandToHand;
        public string IconResref => "skl_handtohand";
        public FeatType LoreFeat => FeatType.HandToHandLore;
        public FeatType PassiveFeat => FeatType.AsuranFists;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Claw,
        ];
    }
}
