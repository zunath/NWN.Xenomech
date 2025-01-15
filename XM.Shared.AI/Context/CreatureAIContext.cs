using XM.AI.BehaviorTree;
using XM.Shared.API.BaseTypes;
using Time = Anvil.API.Time;

namespace XM.AI.Context
{
    internal class CreatureAIContext : IClock
    {
        public AIService AIService { get; }

        public uint Creature { get; }

        public AIFlag AIFlag { get; }

        public Location HomeLocation { get; }

        public uint SelectedItem { get; set; }

        public CreatureAIContext(
            uint creature, 
            AIService ai)
        {
            AIService = ai;
            Creature = creature;
            AIFlag = ai.GetAIFlags(creature);
            HomeLocation = GetLocation(creature);
        }

        public long GetTimeStampInMilliseconds()
        {
            return (long)Time.DeltaTime.TotalMilliseconds;
        }
    }
}
