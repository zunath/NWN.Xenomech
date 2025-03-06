using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class RifleCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Rifle;
        public LocaleString Name => LocaleString.Rifle;
        public string IconResref => "skl_rifle";
        public FeatType LoreFeat => FeatType.RifleLore;
        public FeatType PassiveFeat => FeatType.Trueflight;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Rifle,
        ];
    }
}
