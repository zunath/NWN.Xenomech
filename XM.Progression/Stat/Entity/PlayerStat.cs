using Anvil.Services;
using XM.Data.Shared;
using XM.Progression.Job;

namespace XM.Progression.Stat.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerStat : EntityBase
    {
        public PlayerStat()
        {
            Init();
        }

        public PlayerStat(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {

        }

        public JobType CurrentJob { get; set; }
        public int MaxHP { get; set; }
        public int MaxEP { get; set; }
        public int HP { get; set; }
        public int EP { get; set; }
        public int AbilityRecastReduction { get; set; }

    }
}
