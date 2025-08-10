using Anvil.Services;
using XM.Shared.Core.Entity.Stat;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerStat : EntityBase
    {
        public PlayerStat()
        {
            BaseStats = new StatGroup();
            JobStats = new StatGroup();
            EquippedItemStats = new ItemStatCollection();
            AbilityStats = new AbilityStatCollection();
        }

        public PlayerStat(string playerId)
        {
            Id = playerId;
            BaseStats = new StatGroup();
            JobStats = new StatGroup();
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
