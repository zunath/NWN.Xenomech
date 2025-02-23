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
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Club,
            BaseItemType.LightMace
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.ShiningStrike},
            {240, FeatType.SeraphStrike},
            {540, FeatType.Brainshaker},
            {860, FeatType.BlackHalo},
            {1130, FeatType.FlashNova},
            {1390, FeatType.Judgment},
            {1430, FeatType.HexaStrike},
        };
    }
}
