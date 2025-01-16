using XM.AI.BehaviorTree.FluentBuilder;

namespace XM.AI.Context.Condition
{
    internal static class EnmityConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionSelectHighestEnmityTarget(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Condition("Select Highest Enmity Target", context =>
            {
                var target = context.EnmityService.GetHighestEnmityTarget(context.Creature);

                if (!GetIsObjectValid(target))
                    return false;

                context.SelectedTarget = target;

                return true;
            });
        }    
    }
}
