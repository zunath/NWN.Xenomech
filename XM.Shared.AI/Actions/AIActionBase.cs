
using System;
using XM.AI.Targeters;
using XM.Shared.API.Constants;

namespace XM.AI.Actions
{
    internal abstract class AIActionBase: IAIAction
    {
        private const int ScoreMax = 100;
        private const int ScoreMin = 0;
        protected IAIContext Context { get; }

        protected IAITargeter Targeter { get; private set; }

        public virtual void Initialize()
        {

        }

        protected abstract float CalculateScore();

        public float DetermineScore()
        {
            var score = CalculateScore();

            if (score > ScoreMax)
            {
                score = ScoreMax;
                Console.WriteLine($"WARNING: AI score for '{GetType()}' is above max ({ScoreMax}).");
            }
            else if (score < ScoreMin)
            {
                score = ScoreMin;
                Console.WriteLine($"WARNING: AI score for '{GetType()}' is below min ({ScoreMin})");
            }

            return score;
        }

        public abstract void Execute();

        protected AIActionBase(IAIContext context, IAITargeter targeter)
        {
            Context = context;
            Targeter = targeter;
        }

        protected bool CanUseAbility(FeatType feat, uint target)
        {
            var creature = Context.Creature;
            if (!GetHasFeat(feat, creature))
                return false;

            return Context.Services.Ability.CanUseAbility(Context.Creature, target, feat, GetLocation(target));
        }

        protected void UseAbility(FeatType feat, uint target)
        {
            AssignCommand(Context.Creature, () => ClearAllActions());
            AssignCommand(Context.Creature, () => ActionUseFeat(feat, target));
        }
    }
}
