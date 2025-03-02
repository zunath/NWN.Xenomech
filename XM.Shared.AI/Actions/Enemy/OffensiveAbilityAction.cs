using XM.AI.Targeters;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI.Actions.Enemy
{
    internal class OffensiveAbilityAction: AIActionBase
    {
        public OffensiveAbilityAction(IAIContext context) 
            : base(context, new HighestEnmityTargeter())
        {
        }

        private FeatType _selectedFeat = FeatType.Invalid;
        private uint _target = OBJECT_INVALID;

        protected override float CalculateScore()
        {
            _target = Context.Services.Enmity.GetHighestEnmityTarget(Context.Creature);
            if (_target == OBJECT_INVALID)
                return 0;

            var queuedAbility = Context.Services.Ability.GetQueuedAbility(Context.Creature);
            if (queuedAbility != null)
                return 0;

            if (FindBestAvailableAbility())
                return 60; // Medium-high priority

            return 0;
        }

        private bool FindBestAvailableAbility()
        {
            var feats = Context.GetFeatsByType(AbilityCategoryType.Offensive, AITargetType.Others);
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
