using Anvil.Services;
using XM.Data;

namespace XM.Migration.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class ServerMigrationStatus: EntityBase
    {
        public const string MigrationIdName = "SERVER_MIGRATION";

        public ServerMigrationStatus()
        {
            Id = MigrationIdName;
            MigrationVersion = 0;
        }

        [Indexed]
        public int MigrationVersion { get; set; }
    }
}
