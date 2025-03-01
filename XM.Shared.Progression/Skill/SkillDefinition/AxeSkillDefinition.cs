using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class AxeSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Axe;
        public LocaleString Name => LocaleString.Axe;
        public string IconResref => "skl_axe";
        public FeatType LoreFeat => FeatType.AxeLore;
        public FeatType PassiveFeat => FeatType.PrimalRend;

        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.BattleAxe,
            BaseItemType.HandAxe,
            BaseItemType.DwarvenWarAxe
        ];
    }
}
