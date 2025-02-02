using XM.Progression.Job;
using XM.Shared.API.Constants;

namespace XM.Progression.UI.JobUI
{
    internal class JobEquippedAbility
    {
        public JobType Job { get; set; }
        public int Level { get; set; }

        public JobEquippedAbility(JobType job, int level)
        {
            Job = job;
            Level = level;
        }
    }
}
