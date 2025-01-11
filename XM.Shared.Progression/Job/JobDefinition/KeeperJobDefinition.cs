using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal class KeeperJobDefinition: IJobDefinition
    {
        public LocaleString Name => LocaleString.Keeper;

        public string IconResref => "keeper_icon";

        public JobGrade Grades { get; } = new()
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
    }
}
