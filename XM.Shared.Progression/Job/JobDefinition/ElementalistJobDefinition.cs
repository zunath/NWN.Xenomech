using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class ElementalistJobDefinition: JobDefinitionBase
    {
        public override bool IsVisibleToPlayers => true;

        public override LocaleString Name => LocaleString.Elementalist;

        public override string IconResref => "elem_icon";

        public override JobGrade Grades { get; } = new()
        {
            HP = GradeType.F,
            EP = GradeType.B,
            Might = GradeType.F,
            Perception = GradeType.C,
            Vitality = GradeType.F,
            Agility = GradeType.C,
            Willpower = GradeType.A,
            Social = GradeType.D
        };

        public override Dictionary<int, FeatType> FeatAcquisitionLevels => new()
        {

        };
    }
}
