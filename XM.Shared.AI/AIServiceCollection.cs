using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.Core.Activity;

namespace XM.AI
{
    [ServiceBinding(typeof(AIServiceCollection))]
    internal class AIServiceCollection
    {
        public AbilityService Ability { get; }
        public ActivityService Activity { get; }
        public EnmityService Enmity { get; }
        public StatService Stat { get; }

        public AIServiceCollection(
            AbilityService ability,
            ActivityService activity,
            EnmityService enmity,
            StatService stat)
        {
            Ability = ability;
            Activity = activity;
            Enmity = enmity;
            Stat = stat;
        }
    }
}
