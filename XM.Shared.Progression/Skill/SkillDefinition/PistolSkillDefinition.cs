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
            {160, FeatType.QuickDraw},
            {240, FeatType.BurningShot},
            {540, FeatType.Ricochet},
            {860, FeatType.PiercingShot},
            {1130, FeatType.ShadowBarrage},
            {1390, FeatType.Deadeye},
            {1430, FeatType.TrueShot},
        };
    }
}
