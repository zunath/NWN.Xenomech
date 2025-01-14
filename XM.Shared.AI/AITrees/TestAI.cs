using System;
using XM.AI.BehaviorTree;
using XM.Shared.API.Constants;

namespace XM.AI.AITrees
{
    internal class TestAI: AITreeBase
    {
        private readonly BehaviorTreeBuilder _builder = new();

        public TestAI(uint creature) 
            : base(creature)
        {
        }

        public override IBehaviorTreeNode Tree => _builder
            .Sequence("my-sequence")
                .Do("action1", t =>
                {
                    AssignCommand(Creature, () => ActionPlayAnimation(AnimationType.LoopingSitCross, 1f, 3f));
                    return BehaviorTreeStatus.Success;
                })
                
            .End()

            .Build();

    }
}
