using System;
using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;
using XM.AI.Context.Condition;
using XM.AI.CreatureBehavior.DoAction;
using XM.Shared.API.Constants;

namespace XM.AI.AITrees
{
    internal class TestAITree: AITreeBase
    {
        public TestAITree(uint creature) 
            : base(creature)
        {
        }

        protected override IBehavior<CreatureAIContext> CreateTree()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .PrioritySelector("root")
                    .Subtree(LowHealthBehavior())
                    .Subtree(SitBehavior())
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> LowHealthBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("recover HP with potion")
                    .ConditionHasHPPercentage(0.5f)
                    .ConditionHasAction(ActionType.Invalid)
                    .Sequence("find and use potion")
                        .ConditionHasAnyItem("potion")
                        .DoUseSelectedItem()
                    .End()
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> SitBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("sit")
                .Condition("not sitting", context => GetCurrentAction(context.Creature) != ActionType.Sit)
                    .DoPlayAnimation(AnimationType.LoopingSitCross, 1f, 2.1f)    
                .End()
                .Build();

        }
    }
}
