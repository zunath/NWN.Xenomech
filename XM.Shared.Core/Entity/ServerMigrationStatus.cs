using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class ServerMigrationStatus: EntityBase
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



