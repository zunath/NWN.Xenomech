using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.CombatSkillDefinition
{
    [ServiceBinding(typeof(ICombatSkillDefinition))]
    public class PistolCombatSkillDefinition: ICombatSkillDefinition
    {
        public SkillType Type => SkillType.Pistol;
        public LocaleString Name => LocaleString.Pistol;
        public string IconResref => "skl_pistol";
        public FeatType LoreFeat => FeatType.PistolLore;
        public FeatType PassiveFeat => FeatType.TrueShot;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Pistol,
        ];
    }
}
