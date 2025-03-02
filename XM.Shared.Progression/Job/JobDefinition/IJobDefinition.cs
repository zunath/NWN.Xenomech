using System.Collections.Generic;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.Job.JobDefinition
{
    public interface IJobDefinition
    {
        public JobType Type { get; }
        public ClassType NWNClass { get; }
        public bool IsVisibleToPlayers { get; }
        public LocaleString Name { get; }
        public string IconResref { get; }
        public StatGrade Grades { get; }
        public Dictionary<int, FeatType> FeatAcquisitionLevels { get; }
        public int GetFeatAcquiredLevel(FeatType feat);
    }
}
