using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class PistolSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Pistol;
        public LocaleString Name => LocaleString.Pistol;
        public string IconResref => "skl_pistol";
        public FeatType LoreFeat => FeatType.PistolLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Pistol,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.QuickDraw},
            {160, FeatType.Ricochet},
            {240, FeatType.TrickShot},
            {320, FeatType.CrackShot},
            {540, FeatType.BurningShot},
            {860, FeatType.FreezingShot},
            {1130, FeatType.ShadowBarrage},
            {1390, FeatType.PiercingShot},
            {1430, FeatType.TrueShot},
            {1500, FeatType.Deadeye},
        };
    }
}
