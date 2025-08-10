using System.Collections.Generic;
using System.Text.Json.Serialization;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Entity
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
            ActiveJobCode = 0; // corresponds to JobType.Invalid in Progression
            JobLevels = new JobLevelMap();
            JobXP = new JobXPMap();

            // Populate from JobType domain if available
            var jobMap = KeyNameRegistry.GetCodeToNameMap("JobType");
            if (jobMap != null)
            {
                foreach (var code in jobMap.Keys)
                {
                    if (code == 0) continue; // skip Invalid
                    JobLevels[code] = 1;
                    JobXP[code] = 0;
                }
            }

            ResonanceFeats = new List<FeatType>();
        }

        // Active job and maps stored as int codes to avoid dependency on Progression.Job.JobType
        public int ActiveJobCode { get; set; }
        public JobLevelMap JobLevels { get; set; }
        public JobXPMap JobXP { get; set; }
        public List<FeatType> ResonanceFeats { get; set; }
    }
}
