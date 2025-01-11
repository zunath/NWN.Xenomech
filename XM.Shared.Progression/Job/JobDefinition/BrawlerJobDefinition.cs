using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class BrawlerJobDefinition: IJobDefinition
    {
        public LocaleString Name => LocaleString.Brawler;

        public string IconResref => "brawler_icon";

        public JobGrade Grades { get; } = new()
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
    }
}
