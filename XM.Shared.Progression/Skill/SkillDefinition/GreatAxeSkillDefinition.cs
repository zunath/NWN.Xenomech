using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class GreatAxeSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.GreatAxe;
        public LocaleString Name => LocaleString.GreatAxe;
        public string IconResref => "skl_greataxe";
        public FeatType LoreFeat => FeatType.GreatAxeLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.GreatAxe
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.ShieldBreak},
            {240, FeatType.IronTempest},
            {540, FeatType.FellCleave},
            {860, FeatType.GrandSlash},
            {1130, FeatType.Knockout},
            {1390, FeatType.FurySlash},
            {1430, FeatType.Upheaval},
        };
    }
}
