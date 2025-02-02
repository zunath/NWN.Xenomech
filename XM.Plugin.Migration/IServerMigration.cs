namespace XM.Migration
{
    public interface IServerMigration
    {
        int Version { get; }
        void Migrate();
    }
}
