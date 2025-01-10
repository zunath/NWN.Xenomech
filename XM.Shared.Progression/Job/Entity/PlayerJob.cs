using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Progression.Job.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerJob: EntityBase
    {
        public PlayerJob()
        {
            Init();
        }

        public PlayerJob(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            ActiveJob = JobType.Invalid;
            JobLevels = new Dictionary<JobType, int>();
            JobXP = new Dictionary<JobType, int>();
        }

        public JobType ActiveJob { get; set; }
        public Dictionary<JobType, int> JobLevels { get; set; }
        public Dictionary<JobType, int> JobXP { get; set; }
    }
}
