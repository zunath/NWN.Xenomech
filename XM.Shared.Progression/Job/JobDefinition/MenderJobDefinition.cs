namespace XM.Progression.Job.JobDefinition
{
    internal class MenderJobDefinition: IJobDefinition
    {
        public JobGrade Grades { get; } = new()
        {
            HP = GradeType.E,
            EP = GradeType.C,
            Might = GradeType.D,
            Perception = GradeType.F,
            Vitality = GradeType.D,
            Agility = GradeType.E,
            Willpower = GradeType.A,
            Social = GradeType.C
        };
    }
}
