using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class GreatSwordSkillDefinition: ISkillDefinition
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
