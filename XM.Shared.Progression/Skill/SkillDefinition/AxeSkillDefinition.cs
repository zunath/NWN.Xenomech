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

        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.BattleAxe,
            BaseItemType.HandAxe,
            BaseItemType.DwarvenWarAxe
        ];

        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.RagingAxe},
            {160, FeatType.SmashAxe},
            {240, FeatType.GaleAxe},
            {320, FeatType.AvalancheAxe},
            {540, FeatType.SpinningAxe},
            {860, FeatType.Rampage},
            {1130, FeatType.Calamity},
            {1390, FeatType.MistralAxe},
            {1430, FeatType.Decimation},
            {1500, FeatType.PrimalRend},
        };

    }
}
