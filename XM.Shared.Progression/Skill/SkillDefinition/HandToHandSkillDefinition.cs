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
            {160, FeatType.Combo},
            {240, FeatType.RagingFists},
            {540, FeatType.HowlingFist},
            {860, FeatType.FinalHeaven},
            {1130, FeatType.DragonBlow},
            {1390, FeatType.OneInchPunch},
            {1430, FeatType.AsuranFists},
        };
    }
}
