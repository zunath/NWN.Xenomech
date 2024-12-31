namespace XM.Core.EventManagement.XMEvent
{
    public interface IPlayerMigrationBeforeEvent: IXMEvent
    {
        void OnPlayerMigrationBefore();
    }
}