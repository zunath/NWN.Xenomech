using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Ally
{
    internal class HPHealAllyAction : AIActionBase
    {
        public HPHealAllyAction(IAIContext context)
            : base(context, new LowestHPAllyTargeter())
        {

        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            _target = Targeter.SelectTarget(Context);
            var hp = (float)GetCurrentHitPoints(_target) / (float)GetMaxHitPoints(_target);
            var score = 0;

            if (hp <= 0.2f)
                score = 70;
            else if (hp <= 0.5f)
                score = 40;

            if (score > 0)
                score = FindBestAvailableAbility() ? score : 0;

            return score;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.HPRestoration, AITargetType.Others);
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