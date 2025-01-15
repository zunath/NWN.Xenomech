using Anvil.API;
using XM.AI.BehaviorTree;

namespace XM.AI.Context
{
    internal class CreatureAIContext : IClock
    {
        public uint Creature { get; }

        public uint SelectedItem { get; set; }

        public CreatureAIContext(uint creature)
        {
            Creature = creature;
        }

        public long GetTimeStampInMilliseconds()
        {
            return (long)Time.DeltaTime.TotalMilliseconds;
        }
    }
}
