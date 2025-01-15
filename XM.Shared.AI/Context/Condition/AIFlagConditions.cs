using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.Constants;

namespace XM.AI.Context.Condition
{
    internal static class AIFlagConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasAIFlag(
            this FluentBuilder<CreatureAIContext> builder,
            AIFlag flag)
        {
            return builder.Condition("Has Flag", context => context.AIFlag.HasFlag(flag));
        }

    }
}
