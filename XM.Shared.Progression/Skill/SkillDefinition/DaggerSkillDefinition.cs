using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class DaggerSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Dagger;
        public LocaleString Name => LocaleString.Dagger;
        public string IconResref => "skl_dagger";
        public FeatType LoreFeat => FeatType.DaggerLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Dagger
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.WaspSting},
            {240, FeatType.GustSlash},
            {540, FeatType.Cyclone},
            {860, FeatType.SharkBite},
            {1130, FeatType.Shadowstitch},
            {1390, FeatType.EnergyDrain},
            {1430, FeatType.DancingEdge},
        };
    }
}
