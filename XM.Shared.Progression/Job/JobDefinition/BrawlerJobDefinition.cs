using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class BrawlerJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Brawler;

        public override string IconResref => "brawler_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.A,
            EP = GradeType.G,
            Might = GradeType.C,
            Perception = GradeType.B,
            Vitality = GradeType.A,
            Agility = GradeType.F,
            Willpower = GradeType.D,
            Social = GradeType.E
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
