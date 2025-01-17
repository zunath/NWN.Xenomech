using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class InvalidJobDefinition : JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => false;

        public override LocaleString Name => LocaleString.Empty;

        public override string IconResref => string.Empty;

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.G,
            EP = GradeType.G,
            Might = GradeType.G,
            Perception = GradeType.G,
            Vitality = GradeType.G,
            Agility = GradeType.G,
            Willpower = GradeType.G,
            Social = GradeType.G
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
