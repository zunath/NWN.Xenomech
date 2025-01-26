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
            {50, FeatType.ShiningStrike},
            {160, FeatType.SeraphStrike},
            {240, FeatType.Brainshaker},
            {320, FeatType.Starlight},
            {540, FeatType.Moonlight},
            {860, FeatType.Skullbreaker},
            {1130, FeatType.Judgment},
            {1390, FeatType.HexaStrike},
            {1430, FeatType.BlackHalo},
            {1500, FeatType.FlashNova},
        };
    }
}
