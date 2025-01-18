using System;
using XM.AI.BehaviorTree.FluentBuilder;

namespace XM.AI.Context.Condition
{
    internal static class TargetingConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasEnmity(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Condition("Has Enmity", context =>
            {
                return context.EnmityService.HasEnmity(context.Creature);
            });
        }

        public static FluentBuilder<CreatureAIContext> ConditionSelectHighestEnmityTarget(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Condition("Select Highest Enmity Target", context =>
            {
                var target = context.EnmityService.GetHighestEnmityTarget(context.Creature);

                // Target is invalid or target is the same as last check
                if (!GetIsObjectValid(target) || target == context.SelectedTarget)
                    return false;

                context.SelectedTarget = target;

                return true;
            });
        }    
    }
}
