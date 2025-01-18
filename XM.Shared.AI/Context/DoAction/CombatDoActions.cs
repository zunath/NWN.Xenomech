using System;
using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.Shared.API.Constants;

namespace XM.AI.Context.DoAction
{
    internal static class CombatDoActions
    {
        public static FluentBuilder<CreatureAIContext> DoAttackSelectedTarget(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Do("Attack Selected Target", context =>
            {
                if (GetAttackTarget(context.Creature) != context.SelectedTarget)
                    AssignCommand(context.Creature, () => ClearAllActions());

                AssignCommand(context.Creature, () => ActionAttack(context.SelectedTarget));

                return BehaviorStatus.Succeeded;
            });
        }

        public static FluentBuilder<CreatureAIContext> DoUseAbilityOnSelectedTarget(
            this FluentBuilder<CreatureAIContext> builder,
            FeatType feat)
        {
            return builder.Do("Use Ability on Selected Target", context =>
            {
                AssignCommand(context.Creature, () => ActionUseFeat(feat, context.SelectedTarget));

                return BehaviorStatus.Succeeded;
            });
        }
        public static FluentBuilder<CreatureAIContext> DoUseAbilityOnSelf(
            this FluentBuilder<CreatureAIContext> builder,
            FeatType feat)
        {
            return builder.Do("Use Ability on Self", context =>
            {
                AssignCommand(context.Creature, () => ActionUseFeat(feat, context.Creature));

                return BehaviorStatus.Succeeded;
            });
        }
    }
}
