using XM.AI.BehaviorTree;
using XM.AI.BehaviorTree.FluentBuilder;
using XM.AI.Context;
using XM.AI.Context.Condition;
using XM.AI.Context.DoAction;
using XM.AI.Enmity;
using XM.Progression.Stat;
using XM.Shared.API.Constants;

namespace XM.AI.AITrees
{
    internal class StandardAITree: AITreeBase
    {
        public StandardAITree(
            uint creature, 
            AIService ai, 
            EnmityService enmity, 
            StatService stat) 
            : base(creature, ai, enmity, stat)
        {
        }

        protected override IBehavior<CreatureAIContext> CreateTree()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .Sequence("root")
                    .ConditionAIEnabled()
                    .PrioritySelector("actions")
                        .Subtree(CombatBehavior())
                        .Subtree(SelfPreservationBehavior())
                        .Subtree(OutOfCombatBehavior())
                    .End()
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> CombatBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .PrioritySelector("Combat")
                    .ConditionHasEnmity()
                    .Subtree(TargetingBehavior())
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> SelfPreservationBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .PrioritySelector("Self Preservation")
                    .Subtree(LowHealthBehavior())
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> OutOfCombatBehavior()
        {
            return FluentBuilder.Create<CreatureAIContext>()
                .PrioritySelector("Out of Combat")
                    .Subtree(ReturnHomeBehavior())
                .End()
                .Build();
        }

        private IBehavior<CreatureAIContext> TargetingBehavior()
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
                .Sequence("Recover HP")
                    .ConditionHasHPPercentage(0.5f)
                    .Sequence("Ether Bloom")
                        .ConditionHasFeat(FeatType.EtherBloom1)
                        .DoUseAbilityOnSelf(FeatType.EtherBloom1)
                    .End()
                    .Sequence("Find and Use Potion")
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
