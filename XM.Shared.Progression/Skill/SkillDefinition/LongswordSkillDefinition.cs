using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class LongswordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Longsword;
        public LocaleString Name => LocaleString.Longsword;
        public string IconResref => "skl_longsword";
        public FeatType LoreFeat => FeatType.LongswordLore;
        public FeatType PassiveFeat => FeatType.ShiningBlade;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Longsword,
        ];
    }
}
