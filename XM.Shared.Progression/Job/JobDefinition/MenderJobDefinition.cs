using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class MenderJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Mender;

        public override string IconResref => "mender_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.C,
            Might = GradeType.D,
            Perception = GradeType.F,
            Vitality = GradeType.D,
            Agility = GradeType.E,
            Willpower = GradeType.A,
            Social = GradeType.C
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
