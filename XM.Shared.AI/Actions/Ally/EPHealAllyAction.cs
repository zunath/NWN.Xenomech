using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Ally
{
    internal class EPHealAllyAction : AIActionBase
    {
        public EPHealAllyAction(IAIContext context)
            : base(context, new LowestEPAllyTargeter()) 
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            var stat = Context.Services.Stat;

            _target = Targeter.SelectTarget(Context);
            if (_target == OBJECT_INVALID)
                return 0;

            var ep = (float)stat.GetCurrentEP(_target) / (float)stat.GetMaxEP(_target);
            var score = 0;

            if (ep <= 0.2f)
                score = 85; // High priority if ally EP is critically low
            else if (ep <= 0.5f)
                score = 45; // Medium priority if ally EP is moderately low

            if (score > 0)
                score = FindBestAvailableAbility() ? score : 0;

            return score;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.EPRestoration, AITargetType.Others);
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