namespace XM.Progression.Job.JobDefinition
{
    internal class NightstalkerJobDefinition: IJobDefinition
    {
        public JobGrade Grades { get; } = new()
        {
            HP = GradeType.D,
            EP = GradeType.G,
            Might = GradeType.D,
            Perception = GradeType.A,
            Vitality = GradeType.D,
            Agility = GradeType.B,
            Willpower = GradeType.G,
            Social = GradeType.G
        };
    }
}
