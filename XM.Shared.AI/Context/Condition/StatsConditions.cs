using XM.AI.BehaviorTree.FluentBuilder;

namespace XM.AI.Context.Condition
{
    internal static class StatsConditions
    {
        public static FluentBuilder<CreatureAIContext> ConditionHasHPPercentage(
            this FluentBuilder<CreatureAIContext> builder,
            float percentage)
        {
            return builder.Condition("Has HP Percentage", context =>
            {
                var ratio = (float)GetCurrentHitPoints(context.Creature) / (float)GetMaxHitPoints(context.Creature);
                return ratio <= percentage;
            });
        }
        public static FluentBuilder<CreatureAIContext> ConditionHasEPPercentage(
            this FluentBuilder<CreatureAIContext> builder,
            float percentage)
        {
            return builder.Condition("Has EP Percentage", context =>
            {
                var ratio = (float)context.StatService.GetCurrentEP(context.Creature) /
                            (float)context.StatService.GetMaxEP(context.Creature);

                return ratio <= percentage;
            });
        }
    }
}
