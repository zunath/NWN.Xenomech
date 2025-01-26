using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class HandToHandSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.HandToHand;
        public LocaleString Name => LocaleString.HandToHand;
        public string IconResref => "skl_handtohand";
        public FeatType LoreFeat => FeatType.HandToHandLore;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Claw,
        ];
        public Dictionary<int, FeatType> WeaponSkillAcquisitionLevels { get; } = new()
        {
            {50, FeatType.Combo},
            {160, FeatType.OneInchPunch},
            {240, FeatType.BackhandBlow},
            {320, FeatType.RagingFists},
            {540, FeatType.SpinningAttack},
            {860, FeatType.HowlingFist},
            {1130, FeatType.ParadiseStrike},
            {1390, FeatType.DragonBlow},
            {1430, FeatType.FinalHeaven},
            {1500, FeatType.AsuranFists},
        };
    }
}
