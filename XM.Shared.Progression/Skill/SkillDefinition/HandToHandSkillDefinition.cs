using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class HandToHandSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.HandToHand;
        public LocaleString Name => LocaleString.HandToHand;
        public string IconResref => "skl_handtohand";
        public FeatType LoreFeat => FeatType.HandToHandLore;
        public FeatType PassiveFeat => FeatType.AsuranFists;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Claw,
        ];
    }
}
