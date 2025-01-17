﻿using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;

namespace XM.AI.Context.DoAction
{
    internal static class MovementDoActions
    {
        public static FluentBuilder<CreatureAIContext> DoMoveHome(
            this FluentBuilder<CreatureAIContext> builder)
        {
            return builder.Do("Move Home", context =>
            {
                AssignCommand(context.Creature, () => ActionMoveToLocation(context.HomeLocation));
                return BehaviorStatus.Succeeded;
            });
        }
    }
}
