using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    internal interface IJobDefinition
    {
        public bool IsVisibleToPlayers { get; }
        public LocaleString Name { get; }
        public string IconResref { get; }
        public JobGrade Grades { get; }
    }
}
