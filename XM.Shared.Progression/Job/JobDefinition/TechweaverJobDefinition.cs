using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class TechweaverJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Techweaver;

        public override string IconResref => "techweaver_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.D,
            EP = GradeType.D,
            Might = GradeType.D,
            Perception = GradeType.D,
            Vitality = GradeType.E,
            Agility = GradeType.E,
            Willpower = GradeType.C,
            Social = GradeType.D
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
