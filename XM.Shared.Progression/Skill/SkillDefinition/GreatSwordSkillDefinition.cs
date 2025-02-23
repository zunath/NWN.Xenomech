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
            {160, FeatType.HardSlash},
            {240, FeatType.Frostbite},
            {540, FeatType.SickleMoon},
            {860, FeatType.SpinningSlash},
            {1130, FeatType.ShockSlash},
            {1390, FeatType.GroundStrike},
            {1430, FeatType.Scourge},
        };
    }
}
