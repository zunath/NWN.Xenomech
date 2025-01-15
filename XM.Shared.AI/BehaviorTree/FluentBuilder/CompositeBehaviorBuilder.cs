using System.Collections.Generic;
using System.Linq;

namespace XM.AI.BehaviorTree.FluentBuilder
{
    public sealed class CompositeBehaviorBuilder<TContext> : BehaviorBuilder<TContext>
    {
        public CompositeBehaviorBuilder()
        {
            Children = new List<BehaviorBuilder<TContext>>();
        }

        public CreateCompositeBehavior<TContext> Factory { get; set; }
        public IList<BehaviorBuilder<TContext>> Children { get; }

        public override IBehavior<TContext> Build()
        {
            var behaviours = Children
                .Select(x => x.Build())
                .ToArray();

            return Factory(behaviours);
        }
    }
}