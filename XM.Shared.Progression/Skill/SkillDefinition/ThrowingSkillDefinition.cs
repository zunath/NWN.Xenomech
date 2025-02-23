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
            {160, FeatType.StoneToss},
            {240, FeatType.WindSlash},
            {540, FeatType.ShurikenStorm},
            {860, FeatType.PhantomHurl},
            {1130, FeatType.FlameToss},
            {1390, FeatType.FrostDart},
            {1430, FeatType.StarStrike},
        };
    }
}
