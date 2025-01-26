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
            {50, FeatType.WaspSting},
            {160, FeatType.GustSlash},
            {240, FeatType.Shadowstitch},
            {320, FeatType.Cyclone},
            {540, FeatType.EnergyDrain},
            {860, FeatType.Evisceration},
            {1130, FeatType.MercyStroke},
            {1390, FeatType.RuthlessStroke},
            {1430, FeatType.SharkBite},
            {1500, FeatType.DancingEdge},
        };
    }
}
