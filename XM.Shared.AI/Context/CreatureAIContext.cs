using System.Collections.Generic;
using Anvil.API;
using XM.AI.BehaviorTree;
using XM.AI.Enmity;
using XM.Progression.Stat;
using Time = Anvil.API.Time;

namespace XM.AI.Context
{
    internal class CreatureAIContext : IClock
    {
        public bool IsAIEnabled { get; set; }
        public AIService AIService { get; }

        public EnmityService EnmityService { get; }

        public StatService StatService { get; }

        public uint Creature { get; }

        public AIFlag AIFlag { get; }

        public Location HomeLocation { get; }

        public uint SelectedItem { get; set; }

        public uint SelectedTarget { get; set; }

        public HashSet<uint> NearbyFriendlies { get; set; }

        public CreatureAIContext(
            uint creature, 
            AIService ai,
            EnmityService enmity,
            StatService stat)
        {
            IsAIEnabled = true;
            AIService = ai;
            EnmityService = enmity;
            StatService = stat;
            Creature = creature;
            AIFlag = ai.GetAIFlags(creature);
            HomeLocation = GetLocation(creature);
            NearbyFriendlies = new HashSet<uint>();
        }

        public long GetTimeStampInMilliseconds()
        {
            return (long)Time.DeltaTime.TotalMilliseconds;
        }
    }
}
