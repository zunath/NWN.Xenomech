using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class GreatAxeSkillDefinition: ISkillDefinition
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
