using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.Constants;

namespace XM.AI.Context.Condition
{
    internal static class FeatConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasFeat(
            this FluentBuilder<CreatureAIContext> builder,
            FeatType feat)
        {
            return builder.Condition("Has Feat", context => 
                GetHasFeat(feat, context.Creature));
        }
    }
}
