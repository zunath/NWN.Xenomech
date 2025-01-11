using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class HunterJobDefinition: IJobDefinition
    {
        public bool IsVisibleToPlayers => true;

        public LocaleString Name => LocaleString.Hunter;

        public string IconResref => "hunter_icon";

        public JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.G,
            Might = GradeType.E,
            Perception = GradeType.D,
            Vitality = GradeType.D,
            Agility = GradeType.A,
            Willpower = GradeType.D,
            Social = GradeType.E
        };
    }
}
