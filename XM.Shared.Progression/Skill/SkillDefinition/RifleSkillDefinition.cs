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
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Rifle,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.HotShot},
            {160, FeatType.SplitShot},
            {240, FeatType.SniperShot},
            {320, FeatType.SlugShot},
            {540, FeatType.BlastShot},
            {860, FeatType.HeavyShot},
            {1130, FeatType.Detonator},
            {1390, FeatType.NumbingShot},
            {1430, FeatType.Trueflight},
            {1500, FeatType.LastStand},
        };
    }
}
