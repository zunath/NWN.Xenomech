using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class BeastmasterJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Beastmaster;

        public override string IconResref => "beast_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.C,
            EP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.C,
            Vitality = GradeType.D,
            Agility = GradeType.F,
            Willpower = GradeType.E,
            Social = GradeType.A
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
