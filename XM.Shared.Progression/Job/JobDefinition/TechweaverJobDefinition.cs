using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class TechweaverJobDefinition: IJobDefinition
    {
        public LocaleString Name => LocaleString.Techweaver;

        public string IconResref => "techweaver_icon";

        public JobGrade Grades { get; } = new()
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
    }
}
