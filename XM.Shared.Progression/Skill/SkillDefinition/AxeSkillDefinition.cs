using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class AxeSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Axe;
        public LocaleString Name => LocaleString.Axe;
        public string IconResref => "skl_axe";
        public FeatType LoreFeat => FeatType.AxeLore;
        public FeatType PassiveFeat => FeatType.PrimalRend;

        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.BattleAxe,
            BaseItemType.HandAxe,
            BaseItemType.DwarvenWarAxe
        ];

        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {160, FeatType.RagingAxe},
            {240, FeatType.SmashAxe},
            {540, FeatType.GaleAxe},
            {860, FeatType.AvalancheAxe},
            {1130, FeatType.SpinningAxe},
            {1390, FeatType.Rampage},
            {1430, FeatType.PrimalRend},
        };

    }
}
