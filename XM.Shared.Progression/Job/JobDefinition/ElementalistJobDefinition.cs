using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class ElementalistJobDefinition: IJobDefinition
    {
        public LocaleString Name => LocaleString.Elementalist;

        public string IconResref => "elem_icon";

        public JobGrade Grades { get; } = new()
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
    }
}
