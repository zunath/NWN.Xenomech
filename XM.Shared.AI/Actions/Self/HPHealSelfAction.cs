using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Self
{
    internal class HPHealSelfAction: AIActionBase
    {
        public HPHealSelfAction(IAIContext context) 
            : base(context, new SelfTargeter())
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            var creature = Context.Creature;
            var hp = (float)GetCurrentHitPoints(creature) / (float)GetMaxHitPoints(creature);
            var score = 0;

            if (hp <= 0.2f)
                score = 100;
            else if (hp <= 0.5f)
                score = 80;
            else if (hp <= 0.7f)
                score = 30;

            if (score > 0)
                score = FindBestAvailableAbility() ? score : 0;

            return score;
        }

        private bool FindBestAvailableAbility()
        {
            _target = Targeter.SelectTarget(Context);
            var feats = Context.GetFeatsByType(AbilityCategoryType.HPRestoration, AITargetType.Self);
            foreach (var feat in feats)
            {
                if (CanUseAbility(feat, _target))
                {
                    _selectedFeat = feat;
                    return true;
                }
            }

            _selectedFeat = FeatType.Invalid;
            return false;
        }

        public override void Execute()
        {
            UseAbility(_selectedFeat, _target);
        }
    }
}
