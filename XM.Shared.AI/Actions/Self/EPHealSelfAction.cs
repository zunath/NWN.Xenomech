using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Self
{
    internal class EPHealSelfAction: AIActionBase
    {
        public EPHealSelfAction(IAIContext context)
            : base(context, new SelfTargeter()) // Default to self-restoration
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            var stat = Context.Services.Stat;

            _target = Context.Creature;
            var ep = (float)stat.GetCurrentEP(_target) / (float)stat.GetMaxEP(_target);
            var score = 0;

            if (ep <= 0.2f)
                score = 90; // High priority if EP is critically low
            else if (ep <= 0.5f)
                score = 50; // Medium priority if EP is moderately low

            if (score > 0)
                score = FindBestAvailableAbility() ? score : 0;

            return score;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.EPRestoration, AITargetType.Self);
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