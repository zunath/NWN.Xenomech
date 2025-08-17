namespace XM.Shared.Core.EventManagement
{
    public class XMEvent
    {
        public struct OnAreaCreated : IXMEvent
        {
        }
        public struct OnModuleContentChanged : IXMEvent
        {
        }
        public struct OnPCInitialized : IXMEvent
        {
        }
        public struct OnPlayerMigrationAfter : IXMEvent
        {
        }
        public struct OnPlayerMigrationBefore : IXMEvent
        {
        }
        public struct OnServerHeartbeat : IXMEvent
        {
        }
        public struct OnSpawnCreated : IXMEvent
        {
        }
        public struct OnPlayerOpenQuestsMenu : IXMEvent
        {
        }
        public struct OnPlayerOpenAppearanceMenu : IXMEvent
        {
        }
        public struct OnPlayerOpenCodexMenu : IXMEvent
        {
        }
        public struct OnItemHit : IXMEvent
        {
        }
        public struct OnDamageDealt : IXMEvent
        {
            public uint Target { get; }
            public int Damage { get; }

            public OnDamageDealt(uint target, int damage)
            {
                Target = target;
                Damage = damage;
            }
        }
    }
}
