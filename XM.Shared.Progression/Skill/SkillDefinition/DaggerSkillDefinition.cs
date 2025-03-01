using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Skill.SkillDefinition
{
    [ServiceBinding(typeof(ISkillDefinition))]
    public class DaggerSkillDefinition: ISkillDefinition
    {
        public SkillType Type => SkillType.Dagger;
        public LocaleString Name => LocaleString.Dagger;
        public string IconResref => "skl_dagger";
        public FeatType LoreFeat => FeatType.DaggerLore;
        public FeatType PassiveFeat => FeatType.DancingEdge;
        public List<BaseItemType> BaseItems { get; } =
        [
            BaseItemType.Dagger
        ];
    }
}
