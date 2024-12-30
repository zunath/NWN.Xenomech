using Anvil.Services;
using XM.Data;

namespace XM.Migration.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerMigrationStatus: EntityBase
    {
        public PlayerMigrationStatus()
        {
            MigrationVersion = 0;
        }

        public PlayerMigrationStatus(string playerId)
        {
            Id = playerId;
            MigrationVersion = 0;
        }

        [Indexed]
        public int MigrationVersion { get; set; }
    }
}
