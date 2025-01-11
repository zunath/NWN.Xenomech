using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class BeastmasterJobDefinition: IJobDefinition
    {
        public LocaleString Name => LocaleString.Beastmaster;

        public string IconResref => "beast_icon";

        public JobGrade Grades { get; } = new()
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
    }
}
