using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class KeeperJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Keeper;

        public override string IconResref => "keeper_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.C,
            EP = GradeType.F,
            Might = GradeType.B,
            Perception = GradeType.E,
            Vitality = GradeType.A,
            Agility = GradeType.G,
            Willpower = GradeType.C,
            Social = GradeType.C
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
