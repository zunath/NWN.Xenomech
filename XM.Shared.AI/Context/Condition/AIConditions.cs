using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.Constants;

namespace XM.AI.Context.Condition
{
    internal static class AIConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasAIFlag(
            this FluentBuilder<CreatureAIContext> builder,
            AIFlag flag)
        {
            return builder.Condition("Has Flag", context => context.AIFlag.HasFlag(flag));
        }

        public static FluentBuilder<CreatureAIContext> ConditionAIEnabled(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Condition("Has AI Enabled", context => context.IsAIEnabled);
        }
    }
}
