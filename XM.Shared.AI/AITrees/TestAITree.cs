using System;
using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;
using XM.AI.Context.Condition;
using XM.AI.CreatureBehavior.DoAction;
using XM.AI.Enmity;
using XM.Shared.API.Constants;

namespace XM.AI.AITrees
{
    internal class TestAITree: AITreeBase
    {
        public TestAITree(uint creature, AIService ai, EnmityService enmity) 
            : base(creature, ai, enmity)
        {
        }

        protected override IBehavior<CreatureAIContext> CreateTree()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .PrioritySelector("root")
                    .Subtree(LowHealthBehavior())
                    .Subtree(EnmityBehavior())
                    .Subtree(ReturnHomeBehavior())
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> EnmityBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("Attack Highest Enmity")
                    .ConditionSelectHighestEnmityTarget()
                    .DoAttackSelectedTarget()
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> LowHealthBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("recover HP with potion")
                    .ConditionHasHPPercentage(0.5f)
                    .Sequence("find and use potion")
                        .ConditionSelectHasAnyItem("potion")
                        .DoUseSelectedItem()
                    .End()
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> ReturnHomeBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("Return Home")
                    .ConditionDistanceAwayFromHome(15f)
                    .DoMoveHome()
                .End()
                .Build();
        }

    }
}
