using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class ThrowingCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Throwing;
        public LocaleString Name => LocaleString.Throwing;
        public string IconResref => "skl_throwing";
        public FeatType LoreFeat => FeatType.ThrowingLore;
        public FeatType PassiveFeat => FeatType.StarStrike;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Dart,
            BaseItemType.ThrowingAxe,
            BaseItemType.Shuriken
        ];
    }
}
