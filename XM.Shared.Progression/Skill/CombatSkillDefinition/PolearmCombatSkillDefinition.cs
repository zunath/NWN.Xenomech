using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class PolearmCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Polearm;
        public LocaleString Name => LocaleString.Polearm;
        public string IconResref => "skl_polearm";
        public FeatType LoreFeat => FeatType.PolearmLore;
        public FeatType PassiveFeat => FeatType.Drakesbane;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.ShortSpear,
        ];
    }
}
