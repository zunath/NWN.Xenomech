using System.Collections.Generic;
using XM.AI.Actions;
using XM.AI.Actions.Enemy;
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
                new HealSelfAction(context),
                new AttackEnemyAction(context),
                new ReturnHomeAction(context)
            ];
        }

        protected override List<IAIAction> Actions { get; }
    }
}
