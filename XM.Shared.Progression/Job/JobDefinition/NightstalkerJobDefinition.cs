using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class NightstalkerJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Nightstalker;

        public override string IconResref => "night_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.D,
            EP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.A,
            Vitality = GradeType.D,
            Agility = GradeType.B,
            Willpower = GradeType.G,
            Social = GradeType.G
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
