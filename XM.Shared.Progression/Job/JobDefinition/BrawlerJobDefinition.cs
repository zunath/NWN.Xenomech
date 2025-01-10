namespace XM.Progression.Job.JobDefinition
{
    internal class BrawlerJobDefinition: IJobDefinition
    {
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
