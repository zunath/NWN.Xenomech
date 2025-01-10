namespace XM.Progression.Job.JobDefinition
{
    internal class HunterJobDefinition: IJobDefinition
    {
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
