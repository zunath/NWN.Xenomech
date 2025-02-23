using Anvil.Services;
using XM.Shared.Core.Data;

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
            JobStats = new StatGroup();
            BaseStats = new ItemStatGroup();
            EquippedItemStats = new ItemStatCollection();
            AbilityStats = new AbilityStatCollection();
        }

        public int HP { get; set; }
        public int EP { get; set; }
        public int TP { get; set; }
        public StatGroup BaseStats { get; set; }
        public StatGroup JobStats { get; set; }
        public ItemStatCollection EquippedItemStats { get; set; }
        public AbilityStatCollection AbilityStats { get; set; }

    }
}
