using XM.AI.BehaviorTree;
using XM.AI.Enmity;
using XM.Shared.API.BaseTypes;
using Time = Anvil.API.Time;

namespace XM.AI.Context
{
    internal class CreatureAIContext : IClock
    {
        public AIService AIService { get; }

        public EnmityService EnmityService { get; }

        public uint Creature { get; }

        public AIFlag AIFlag { get; }

        public Location HomeLocation { get; }

        public uint SelectedItem { get; set; }

        public uint SelectedTarget { get; set; }

        public CreatureAIContext(
            uint creature, 
            AIService ai,
            EnmityService enmity)
        {
            AIService = ai;
            EnmityService = enmity;
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
