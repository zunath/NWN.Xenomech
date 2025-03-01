using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class BowSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Bow;
        public LocaleString Name => LocaleString.Bow;
        public string IconResref => "skl_bow";
        public FeatType LoreFeat => FeatType.BowLore;
        public FeatType PassiveFeat => FeatType.ApexArrow;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.LongBow,
            BaseItemType.ShortBow
        ];
    }
}
