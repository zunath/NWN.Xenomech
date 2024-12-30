namespace XM.Migration
{
    public interface IPlayerMigration
    {
        int Version { get; }
        void Migrate(uint player);
    }
}
