using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Stat;

namespace XM.AI
{
    [ServiceBinding(typeof(AIServiceCollection))]
    internal class AIServiceCollection
    {
        public AbilityService Ability { get; }
        public EnmityService Enmity { get; }
        public StatService Stat { get; }

        public AIServiceCollection(
            AbilityService ability,
            EnmityService enmity,
            StatService stat)
        {
            Ability = ability;
            Enmity = enmity;
            Stat = stat;
        }
    }
}
