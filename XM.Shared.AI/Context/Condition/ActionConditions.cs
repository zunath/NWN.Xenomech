using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.Constants;

namespace XM.AI.Context.Condition
{
    internal static class ActionConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasAction(
            this FluentBuilder<CreatureAIContext> builder,
            ActionType action)
        {
            return builder.Condition("Has Action", context => GetCurrentAction(context.Creature) == ActionType.Invalid);
        }
    }
}
