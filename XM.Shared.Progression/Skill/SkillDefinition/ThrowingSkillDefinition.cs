using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class ThrowingSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Throwing;
        public LocaleString Name => LocaleString.Throwing;
        public string IconResref => "skl_throwing";
        public FeatType LoreFeat => FeatType.ThrowingLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Dart,
            BaseItemType.ThrowingAxe,
            BaseItemType.Shuriken
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.StoneToss},
            {160, FeatType.WindSlash},
            {240, FeatType.ShurikenStorm},
            {320, FeatType.GaleCutter},
            {540, FeatType.FlameToss},
            {860, FeatType.FrostDart},
            {1130, FeatType.ShockBomb},
            {1390, FeatType.ShadowThrow},
            {1430, FeatType.StarStrike},
            {1500, FeatType.PhantomHurl},
        };
    }
}
