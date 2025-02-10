using System.Collections.Generic;
using XM.AI.Actions;
using XM.AI.Actions.Ally;
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
                // Ally
                new HealAllyAction(context),

                // Enemy
                new AttackEnemyAction(context),

                // Self
                new HealSelfAction(context),
                new ReturnHomeAction(context)
            ];
        }

        protected override List<IAIAction> Actions { get; }
    }
}
