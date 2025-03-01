using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class ClubSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Club;
        public LocaleString Name => LocaleString.Club;
        public string IconResref => "skl_club";
        public FeatType LoreFeat => FeatType.ClubLore;
        public FeatType PassiveFeat => FeatType.HexaStrike;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Club,
            BaseItemType.LightMace
        ];
    }
}
