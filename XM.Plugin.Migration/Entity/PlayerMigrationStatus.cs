using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Migration.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerMigrationStatus: EntityBase
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
