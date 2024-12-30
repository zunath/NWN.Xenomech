namespace XM.Migration
{
    public interface IServerMigration
    {
        int Version { get; }
        MigrationExecutionType ExecutionType { get; }
        void Migrate();
    }
}
