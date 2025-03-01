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
        public FeatType PassiveFeat => FeatType.TrueShot;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Pistol,
        ];
    }
}
