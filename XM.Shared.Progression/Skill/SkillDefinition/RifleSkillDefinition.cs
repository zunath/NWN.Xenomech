using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class RifleSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Rifle;
        public LocaleString Name => LocaleString.Rifle;
        public string IconResref => "skl_rifle";
        public FeatType LoreFeat => FeatType.RifleLore;
        public FeatType PassiveFeat => FeatType.Trueflight;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Rifle,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.HotShot},
            {240, FeatType.SplitShot},
            {540, FeatType.SniperShot},
            {860, FeatType.SlugShot},
            {1130, FeatType.BlastShot},
            {1390, FeatType.HeavyShot},
            {1430, FeatType.Trueflight},
        };
    }
}
