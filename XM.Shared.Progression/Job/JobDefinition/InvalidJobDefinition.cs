using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class InvalidJobDefinition : IJobDefinition
    {
        public LocaleString Name => LocaleString.Empty;

        public string IconResref => string.Empty;

        public JobGrade Grades { get; } = new()
        {
            HP = GradeType.G,
            EP = GradeType.G,
            Might = GradeType.G,
            Perception = GradeType.G,
            Vitality = GradeType.G,
            Agility = GradeType.G,
            Willpower = GradeType.G,
            Social = GradeType.G
        };
    }
}
