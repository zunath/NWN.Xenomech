using System.Collections.Generic;
using Anvil.Services;
using XM.Shared.API.Constants;

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
            JobLevels = new Dictionary<int, int>
            {
                { 1, 1 }, // Keeper
                { 2, 1 }, // Mender
                { 3, 1 }, // Brawler
                { 4, 1 }, // Beastmaster
                { 5, 1 }, // Techweaver
                { 6, 1 }, // Elementalist
                { 7, 1 }, // Nightstalker
                { 8, 1 }, // Hunter
            };
            JobXP = new Dictionary<int, int>
            {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 },
            };

            ResonanceFeats = new List<FeatType>();
        }

        // Active job and maps stored as int codes to avoid dependency on Progression.Job.JobType
        public int ActiveJobCode { get; set; }
        public Dictionary<int, int> JobLevels { get; set; }
        public Dictionary<int, int> JobXP { get; set; }
        public List<FeatType> ResonanceFeats { get; set; }
    }
}
