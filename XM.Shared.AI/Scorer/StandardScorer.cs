using System.Collections.Generic;
using XM.AI.Actions;
using XM.AI.Actions.Self;

namespace XM.AI.Scorer
{
    internal class StandardScorer: ScorerBase
    {
        public StandardScorer(IAIContext context) 
            : base(context)
        {
            Actions = 
            [
                new HealSelfAction(Context)
            ];
        }

        protected override List<IAIAction> Actions { get; }
    }
}
