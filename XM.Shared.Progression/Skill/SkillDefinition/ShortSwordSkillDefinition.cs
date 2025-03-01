using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class ShortSwordSkillDefinition: ISkillDefinition
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
