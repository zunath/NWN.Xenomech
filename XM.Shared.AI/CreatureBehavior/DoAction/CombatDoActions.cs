using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;

namespace XM.AI.CreatureBehavior.DoAction
{
    internal static class CombatDoActions
    {
        public static FluentBuilder<CreatureAIContext> DoAttackSelectedTarget(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Do("Attack Selected Target", context =>
            {
                if(GetAttackTarget(context.Creature) != context.SelectedTarget)
                    AssignCommand(context.Creature, () => ClearAllActions());

                AssignCommand(context.Creature, () => ActionAttack(context.SelectedTarget));

                return BehaviorStatus.Succeeded;
            });
        }
    }
}
