using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class PolearmSkillDefinition: ISkillDefinition
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
