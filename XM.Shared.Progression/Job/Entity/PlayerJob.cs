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
            JobLevels = new Dictionary<JobType, int>()
            {
                { JobType.Keeper, 1},
                { JobType.Mender, 1},
                { JobType.Brawler, 1},
                { JobType.Beastmaster, 1},
                { JobType.Techweaver, 1},
                { JobType.Elementalist, 1},
                { JobType.Nightstalker, 1},
                { JobType.Hunter, 1},
            };
            JobXP = new Dictionary<JobType, int>()
            {
                { JobType.Keeper, 0},
                { JobType.Mender, 0},
                { JobType.Brawler, 0},
                { JobType.Beastmaster, 0},
                { JobType.Techweaver, 0},
                { JobType.Elementalist, 0},
                { JobType.Nightstalker, 0},
                { JobType.Hunter, 0},
            }; ;
        }

        public JobType ActiveJob { get; set; }
        public Dictionary<JobType, int> JobLevels { get; set; }
        public Dictionary<JobType, int> JobXP { get; set; }
    }
}
