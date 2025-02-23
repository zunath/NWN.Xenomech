using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class PolearmSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Polearm;
        public LocaleString Name => LocaleString.Polearm;
        public string IconResref => "skl_polearm";
        public FeatType LoreFeat => FeatType.PolearmLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.ShortSpear,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.DoubleThrust},
            {240, FeatType.ThunderThrust},
            {540, FeatType.RaidenThrust},
            {860, FeatType.PentaThrust},
            {1130, FeatType.VorpalThrust},
            {1390, FeatType.SonicThrust},
            {1430, FeatType.Drakesbane},
        };
    }
}
