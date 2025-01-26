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
            {50, FeatType.ShieldBreak},
            {160, FeatType.IronTempest},
            {240, FeatType.Sturmwind},
            {320, FeatType.KeenEdge},
            {540, FeatType.RagingRush},
            {860, FeatType.FellCleave},
            {1130, FeatType.Upheaval},
            {1390, FeatType.Knockout},
            {1430, FeatType.FurySlash},
            {1500, FeatType.GrandSlash},
        };
    }
}
