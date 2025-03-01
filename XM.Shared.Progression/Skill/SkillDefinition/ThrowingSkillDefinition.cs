using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class ThrowingSkillDefinition: ISkillDefinition
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
