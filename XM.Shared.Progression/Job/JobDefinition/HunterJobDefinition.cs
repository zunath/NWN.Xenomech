using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class HunterJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Hunter;

        public override string IconResref => "hunter_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.G,
            Might = GradeType.E,
            Perception = GradeType.D,
            Vitality = GradeType.D,
            Agility = GradeType.A,
            Willpower = GradeType.D,
            Social = GradeType.E
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
