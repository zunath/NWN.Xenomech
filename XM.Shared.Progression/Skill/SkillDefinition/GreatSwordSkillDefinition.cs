using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class GreatSwordSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.GreatSword;
        public LocaleString Name => LocaleString.GreatSword;
        public string IconResref => "skl_greatsword";
        public FeatType LoreFeat => FeatType.GreatSwordLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.GreatSword
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.HardSlash},
            {160, FeatType.PowerSlash},
            {240, FeatType.Frostbite},
            {320, FeatType.ShockSlash},
            {540, FeatType.SickleMoon},
            {860, FeatType.GroundStrike},
            {1130, FeatType.Freezebite},
            {1390, FeatType.HerculeanSlash},
            {1430, FeatType.SpinningSlash},
            {1500, FeatType.Scourge},
        };
    }
}
